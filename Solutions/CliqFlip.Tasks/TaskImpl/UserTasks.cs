using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Infrastructure.Authentication.Interfaces;
using CliqFlip.Infrastructure.Common;
using CliqFlip.Infrastructure.Extensions;
using CliqFlip.Infrastructure.IO;
using CliqFlip.Infrastructure.IO.Interfaces;
using CliqFlip.Infrastructure.Images;
using CliqFlip.Infrastructure.Images.Interfaces;
using CliqFlip.Infrastructure.Location.Interfaces;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using CliqFlip.Infrastructure.Syndication.Interfaces;
using CliqFlip.Infrastructure.Validation;
using CliqFlip.Infrastructure.Web.Interfaces;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.Domain.Specifications;
using CliqFlip.Infrastructure.Email.Interfaces;

namespace CliqFlip.Tasks.TaskImpl
{
	public class UserTasks : IUserTasks
	{
		private readonly IUserRepository _userRepository;
		private readonly IFeedFinder _feedFinder;
		private readonly IFileUploadService _fileUploadService;
		private readonly IHtmlService _htmlService;
		private readonly IImageProcessor _imageProcessor;
		private readonly IInterestTasks _interestTasks;
		private readonly IUserAuthentication _userAuthentication;
		private readonly IConversationRepository _conversationRepository;
        private readonly IEmailService _emailService;
		private readonly ILocationService _locationService;

		public UserTasks(
						 IInterestTasks interestTasks,
						 IImageProcessor imageProcessor,
						 IFileUploadService fileUploadService,
						 IHtmlService htmlService,
						 IFeedFinder feedFinder,
						 IUserAuthentication userAuthentication,
						 IConversationRepository conversationRepository,
                         IEmailService emailService, ILocationService locationService, IUserRepository userRepository)
		{
			_interestTasks = interestTasks;
			_imageProcessor = imageProcessor;
			_fileUploadService = fileUploadService;
			_htmlService = htmlService;
			_feedFinder = feedFinder;
			_userAuthentication = userAuthentication;
            _conversationRepository = conversationRepository;
            _emailService = emailService;
			_locationService = locationService;
			_userRepository = userRepository;
		}

		#region IUserTasks Members

		public IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IList<string> interestAliases)
		{
			IList<string> subjAliasAndParent = _interestTasks.GetSlugAndParentSlug(interestAliases);

			List<User> users = _userRepository.GetUsersByInterests(subjAliasAndParent).ToList();

            return users.Select(user => new UserSearchByInterestsDto
            {
                MatchCount = user.Interests.Sum(x =>
                {
                    if (interestAliases.Contains(x.Interest.Slug))
                        return 3; //movies -> movies (same match)
                    if (x.Interest.ParentInterest != null && subjAliasAndParent.Contains(x.Interest.ParentInterest.Slug))
                        return 2; //movies -> tv shows (sibling match)
                    if (subjAliasAndParent.Contains(x.Interest.Slug))
                        return 1; //movies -> entertainment (parent match)
                    return 0;
                }),
                UserDto = new UserDto(user)
            }).OrderByDescending(x => x.MatchCount).ToList();
		}

		public User Create(UserDto userToCreate, LocationData location)
		{
			string salt = PasswordHelper.GenerateSalt(16);//TODO: should this be 32
			string pHash = PasswordHelper.GetPasswordHash(userToCreate.Password, salt);

			var user = new User(userToCreate.Username, userToCreate.Email, pHash, salt);

            ProcessUserInterests(user, userToCreate.InterestDtos);

			var majorLocation = _locationService.GetNearestMajorCity(location.Latitude, location.Longitude);

			user.UpdateBio("I ♥ " + string.Join(", ", user.Interests.Select(x => x.Interest.Name)));
			user.UpdateHeadline("I am " + user.Username + ", hear me roar!");
			user.UpdateLocation(location, majorLocation);

			user.UpdateCreateDate();

			_userRepository.SaveOrUpdate(user);

			return user;
		}

		public void Login(User user, bool stayLoggedIn)
		{
			user.UpdateLastActivity();
			_userAuthentication.Login(user.Username, stayLoggedIn, user.Role);
		}

		public bool Login(string username, string password, bool stayLoggedIn)
		{
			bool retVal = false;
			User user = _userRepository.FindByNameOrEmail(username);

			if (user != null)
			{
				//use the users salt and provided password to see if the password match
				string expetectedPassword = PasswordHelper.GetPasswordHash(password, user.Salt);
				if (user.Password == expetectedPassword)
				{
					Login(user, stayLoggedIn);
					retVal = true;
				}
			}

			return retVal;
		}


		public User GetUser(string username)
		{
			return _userRepository.FindByName(username);
		}

		public User GetSuggestedUser(string username)
		{
			var user = GetUser(username);
			return _userRepository.GetSuggestedUser(user);
		}

		public void SaveInterestImage(User user, HttpPostedFileBase interestImage, int userInterestId, string description)
		{
			UserInterest interest = user.Interests.First(x => x.Id == userInterestId);
			SaveImageForUser(interestImage,
							 user.Username + "-Interest-Image-" + interest.Interest.Name,
							 imgFileNamesDto =>
							 interest.AddImage(new ImageData(interestImage.FileName, description, imgFileNamesDto.ThumbFilename, imgFileNamesDto.MediumFilename, imgFileNamesDto.FullFilename)));
		}

		public void SaveWebsite(User user, string siteUrl)
		{
			if (string.IsNullOrWhiteSpace(siteUrl)) throw new ArgumentNullException("siteUrl");

			siteUrl = siteUrl.FormatWebAddress();

			if (!UrlValidation.IsValidUrl(siteUrl)) throw new RulesException("SiteUrl", "Invalid URL");

			string html = _htmlService.GetHtmlFromUrl(siteUrl);

			string feedUrl = _feedFinder.GetFeedUrl(html);

			if (string.IsNullOrWhiteSpace(feedUrl))
			{
				feedUrl = null;
			}

			user.UpdateWebsite(siteUrl, feedUrl);
		}

		public void Logout(string name)
		{
			User user = GetUser(name);

			if (user != null)
			{
				user.UpdateLastActivity();
			}

			_userAuthentication.Logout();
		}

		public void RemoveImage(User user, int imageId)
		{
			Image image = user.GetImage(imageId);
			ImageFileNamesDto originalImageNames = GetImageFileNamesDto(image);
			DeleteImages(originalImageNames);
			user.RemoveInterestImage(image);
		}

		public void RemoveInterest(User user, int interestId)
		{
			var interest = user.GetInterest(interestId);

			var images = interest.Images.ToList();

			var files = new List<ImageFileNamesDto>(images.Count * 3);

			files.AddRange(images.Select(GetImageFileNamesDto));

			DeleteImages(files.ToArray());

			user.RemoveInterest(interest);
		}

		public void AddInterestToUser(User user, int interestId)
		{
			var interest = _interestTasks.Get(interestId);
			user.AddInterest(interest, null);
		}

		public void AddInterestsToUser(string name, IEnumerable<UserInterestDto> interestDtos)
		{
			var user = GetUser(name);
            ProcessUserInterests(user, interestDtos);
		}

		public void SaveProfileImage(User user, HttpPostedFileBase profileImage)
		{
			ImageFileNamesDto originalImageNames = null;
			if (user.ProfileImage != null)
			{
				originalImageNames = GetImageFileNamesDto(user.ProfileImage);
			}

			SaveImageForUser(profileImage, user.Username + "-Profile-Image", imgFileNamesDto =>
			{
				user.UpdateProfileImage(new ImageData(profileImage.FileName, null, imgFileNamesDto.ThumbFilename, imgFileNamesDto.MediumFilename, imgFileNamesDto.FullFilename));
				if (originalImageNames != null)
				{
					DeleteImages(originalImageNames);
				}
			});
		}

		private static ImageFileNamesDto GetImageFileNamesDto(Image image)
		{
			return new ImageFileNamesDto
			{
				ThumbFilename = image.Data.ThumbFileName,
				MediumFilename = image.Data.MediumFileName,
				FullFilename = image.Data.FullFileName
			};
		}

		#endregion

		private void SaveImageForUser(HttpPostedFileBase profileImage, string metaPrefix, Action<ImageFileNamesDto> afterProcessing)
		{
			if (afterProcessing == null) throw new ArgumentNullException("afterProcessing");

			//be very safe with image streams
			var exceptions = new List<Exception>();

			ImageProcessResult result = null;

			try
			{
				var newImageFileNames = new ImageFileNamesDto();

				result = _imageProcessor.ProcessImage(profileImage);
				bool fullFileExists = result.FullImage != null;

				var files = new List<FileToUpload>
				{
					new FileToUpload(result.ThumbnailImage.Image, "thumb_" + profileImage.FileName, metaPrefix + "-" + "Thumb"),
					new FileToUpload(result.MediumImage.Image, "med_" + profileImage.FileName, metaPrefix + "-" + "Medium")
				};

				if (fullFileExists)
				{
					files.Add(new FileToUpload(result.FullImage.Image, "full_" + profileImage.FileName, metaPrefix + "-" + "Full"));
				}

				IList<string> filePaths = _fileUploadService.UploadFiles("Images/", files);

				newImageFileNames.ThumbFilename = filePaths[0];
				newImageFileNames.MediumFilename = filePaths[1];
				newImageFileNames.FullFilename = filePaths[fullFileExists ? 2 : 1];

				afterProcessing(newImageFileNames);
			}
			catch (RulesException)
			{
				throw;
			}
			catch (Exception ex)
			{
				exceptions.Add(ex);
			}
			finally
			{
				if (result != null)
				{
					DisposeImageIfNotEmpty(result.ThumbnailImage, exceptions);
					DisposeImageIfNotEmpty(result.MediumImage, exceptions);
					DisposeImageIfNotEmpty(result.FullImage, exceptions);
				}
			}

			if (exceptions.Any())
			{
				throw new AggregateException("Error processing image", exceptions);
			}
		}

		private void DeleteImages(params ImageFileNamesDto[] imageNames)
		{
			if (imageNames == null) throw new ArgumentNullException("imageNames");

			var filesToDelete = new List<string>(imageNames.Length * 3);

			foreach (var image in imageNames)
			{
				filesToDelete.Add(image.ThumbFilename);
				filesToDelete.Add(image.MediumFilename);

				if (image.FullFilename != image.MediumFilename)
				{
					//only delete if there is truly a full image
					filesToDelete.Add(image.FullFilename);
				}
			}

			_fileUploadService.DeleteFiles("Images/", filesToDelete);
		}

		private void DisposeImageIfNotEmpty(ResizedImage resizedImage, IList<Exception> exceptions)
		{
			if (resizedImage != null)
			{
				try
				{
					resizedImage.Image.Dispose();
				}
				catch (Exception ex)
				{
					exceptions.Add(ex);
				}
			}
		}

        public void StartConversation(string starter, string receiver, string text)
        {
            //get the users involved in the conversation
            User sender = GetUser(starter),
                recipient = GetUser(receiver);

            //check that both users exists
            if (sender == null || recipient == null)
                return;
            
            //get the conversation that the recipient has with the sender, if any
            var conversation = recipient.Conversations.SingleOrDefault(x => x.Users.Any(user => user.Username == starter));
            
            if (conversation == null)
            {
                //start a new conversation
                conversation =  new Conversation(sender, recipient);
            }

            Message message = sender.WriteMessage(text);
            conversation.AddMessage(message);
            _conversationRepository.SaveOrUpdate(conversation);
            _emailService.SendMail(recipient.Email, "Some one likes you on CliqFlip.com", "Hey, <br/> Looks like someone finds you interesting. Go talk to this person at CliqFlip.com");
        }

        public Message ReplyToConversation(int conversationId, string replier, string text)
        {
            Message retVal = null;
            var sender = GetUser(replier);

            if (sender != null)
            {
                var conversation = sender.Conversations.SingleOrDefault(x => x.Id == conversationId);

                if (conversation != null)
                {
                    retVal = sender.WriteMessage(text);
                    conversation.AddMessage(retVal);
                    var users = conversation.Users.ToList();
                    users.Remove(sender);
                    var subject = "You have a new messages on CliqFlip.com :)";
                    var message = "Hey come back, {0} sent you a message.";

                    users.ForEach(user => _emailService.SendMail(user.Email, subject, String.Format(message, user.Username)));
                }
            }
            return retVal;
        }

        public bool IsUsernameOrEmailAvailable(string value)
        {
            return _userRepository.IsUsernameOrEmailAvailable(value);
        }

        private void ProcessUserInterests(User user, IEnumerable<UserInterestDto> interestDtos)
        {
            foreach (var interestDto in interestDtos)
            {
                Interest interest;
                
                if (interestDto.Id > 0)
                    interest = _interestTasks.Get(interestDto.Id);
                else
                    interest = _interestTasks.Create(interestDto.Name, interestDto.RelatedTo);

                user.AddInterest(interest, null);
            }
        }
	}
}