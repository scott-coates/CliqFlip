﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Domain.Interfaces;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Infrastructure.Authentication.Interfaces;
using CliqFlip.Infrastructure.Common;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Extensions;
using CliqFlip.Infrastructure.IO;
using CliqFlip.Infrastructure.IO.Interfaces;
using CliqFlip.Infrastructure.Images;
using CliqFlip.Infrastructure.Images.Interfaces;
using CliqFlip.Infrastructure.Location.Interfaces;
using CliqFlip.Infrastructure.Repositories.Interfaces;
using CliqFlip.Infrastructure.Syndication.Interfaces;
using CliqFlip.Infrastructure.Validation;
using CliqFlip.Infrastructure.Web;
using CliqFlip.Infrastructure.Web.Interfaces;

namespace CliqFlip.Tasks.TaskImpl
{
	public class UserTasks : IUserTasks
	{
		private readonly IConversationRepository _conversationRepository;
		private readonly IEmailService _emailService;
		private readonly IFeedFinder _feedFinder;
		private readonly IFileUploadService _fileUploadService;
		private readonly IWebContentService _webContentService;
		private readonly IImageProcessor _imageProcessor;
		private readonly IInterestTasks _interestTasks; //TODO: consider if this should be here
		private readonly IUserInterestTasks _userInterestTasks;
		private readonly ILocationService _locationService;
		private readonly IPageParsingService _pageParsingService;
		private readonly IUserAuthentication _userAuthentication;
		private readonly IUserRepository _userRepository;

		public UserTasks(
			IInterestTasks interestTasks,
			IImageProcessor imageProcessor,
			IFileUploadService fileUploadService,
			IWebContentService webContentService,
			IFeedFinder feedFinder,
			IUserAuthentication userAuthentication,
			IConversationRepository conversationRepository,
			IEmailService emailService,
			ILocationService locationService,
			IUserRepository userRepository,
			IPageParsingService pageParsingService, IUserInterestTasks userInterestTasks)
		{
			_interestTasks = interestTasks;
			_imageProcessor = imageProcessor;
			_fileUploadService = fileUploadService;
			_webContentService = webContentService;
			_feedFinder = feedFinder;
			_userAuthentication = userAuthentication;
			_conversationRepository = conversationRepository;
			_emailService = emailService;
			_locationService = locationService;
			_userRepository = userRepository;
			_pageParsingService = pageParsingService;
			_userInterestTasks = userInterestTasks;
		}

		#region IUserTasks Members

        public IList<UserSearchByInterestsDto> GetUsersByInterestsDtos(IList<ScoredRelatedInterestDto> interests)
		{
            List<User> users = _userRepository
                .GetUsersByInterests(interests
                    .Select(x=>x.Id)
                    .ToList())
                .ToList();

			return users.Select(user => new UserSearchByInterestsDto
			{
				MatchCount = user.Interests.Sum(x =>
				{
                    var foundInterest = interests.FirstOrDefault(y => y.Id == x.Interest.Id);
				    return foundInterest != null ? foundInterest.Score : 0f;
				}),
				UserDto = new UserDto(user)
			}).OrderByDescending(x => x.MatchCount).ToList();
		}

		public User Create(UserDto userToCreate, LocationData location)
		{
			string salt = PasswordHelper.GenerateSalt(16); //TODO: should this be 32 - encapsulate this somehwere
			string pHash = PasswordHelper.GetPasswordHash(userToCreate.Password, salt);

		    var user = new User(userToCreate.Username, userToCreate.Email, pHash, salt)
		    {
		        CreateDate = DateTime.UtcNow
		    };

			MajorLocation majorLocation = _locationService.GetNearestMajorCity(location.Latitude, location.Longitude);

			user.UpdateBio("I ♥ " + string.Join(", ", user.Interests.Select(x => x.Interest.Name)));
			user.UpdateHeadline("I am " + user.Username + ", hear me roar!");
			user.UpdateLocation(location, majorLocation);

			user.UpdateCreateDate();

			_userRepository.SaveOrUpdate(user);

            ProcessUserInterests(user, userToCreate.InterestDtos);

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
			User user = GetUser(username);
			return _userRepository.GetSuggestedUser(user);
		}

		public void SaveInterestImage(User user, FileStreamDto interestImage, int userInterestId, string description)
		{
			UserInterest interest = user.Interests.First(x => x.Id == userInterestId);
			SaveImageForUser(interestImage,
							 user.Username + "-Interest-Image-" + interest.Interest.Name,
							 imgFileNamesDto =>
							 interest.AddMedium(
								new Image
								{
									Description = description,
									ImageData =
										new ImageData(
											interestImage.FileName
											, imgFileNamesDto.ThumbFilename
											, imgFileNamesDto.MediumFilename
											, imgFileNamesDto.FullFilename),
									CreateDate = DateTime.UtcNow
								}));
		}

		public void SaveInterestImage(User user, int userInterestId, string description, string imageUrl)
		{
			imageUrl = imageUrl.FormatWebAddress();

			//DRY this up..the formatWebAddress, the byte[], the streams (videos, iframe, etc)
			byte[] data = _webContentService.GetDataFromUrl(imageUrl);

			string fileName = Path.GetFileName(imageUrl);

			using (var memoryStream = new MemoryStream(data))
			{
				SaveInterestImage(user, new FileStreamDto(memoryStream, fileName), userInterestId, description);
			}
		}

		public void SaveInterestVideo(User user, int userInterestId, string videoUrl)
		{
			UserInterest interest = user.Interests.First(x => x.Id == userInterestId);

			videoUrl = videoUrl.FormatWebAddress();
			string html = _webContentService.GetHtmlFromUrl(videoUrl);
			PageDetails details = _pageParsingService.GetDetails(html);

			string description = details.Description;

			if (string.IsNullOrWhiteSpace(details.VideoUrl))
			{
				throw new RulesException("Description", "Invalid video");
			}

			var medium = new Video { Description = description, VideoUrl = details.VideoUrl, Title = details.Title, CreateDate = DateTime.UtcNow };

			//determine if image is available
			if (!string.IsNullOrWhiteSpace(details.ImageUrl))
			{
				byte[] data = _webContentService.GetDataFromUrl(details.ImageUrl);
				string fileName = Path.GetFileName(details.ImageUrl);
				using (var memoryStream = new MemoryStream(data))
				{
					var fileStreamDto = new FileStreamDto(memoryStream, fileName);
					SaveImageForUser(fileStreamDto, user.Username + "-Interest-Video-Image-" + interest.Interest.Name, imgFileNamesDto =>
						medium.AddImage(new ImageData(fileName, imgFileNamesDto.ThumbFilename, imgFileNamesDto.MediumFilename, imgFileNamesDto.FullFilename)));
				}
			}

			interest.AddMedium(medium);
		}

		public void SaveInterestWebPage(User user, int userInterestId, string linkUrl)
		{
			UserInterest interest = user.Interests.First(x => x.Id == userInterestId);

			linkUrl = linkUrl.FormatWebAddress();
			string html = _webContentService.GetHtmlFromUrl(linkUrl);
			PageDetails details = _pageParsingService.GetDetails(html);

			string description = details.Description;

			var uri = new Uri(linkUrl);
			var domain = uri.GetLeftPart(UriPartial.Authority).Replace(uri.GetLeftPart(UriPartial.Scheme), "");

			var medium = new WebPage { Description = description, LinkUrl = linkUrl, WebPageDomainName = domain, Title = details.Title, CreateDate = DateTime.UtcNow };

			//determine if image is available
			if (!string.IsNullOrWhiteSpace(details.ImageUrl))
			{
				byte[] data = _webContentService.GetDataFromUrl(details.ImageUrl);
				string fileName = Path.GetFileName(details.ImageUrl);
				using (var memoryStream = new MemoryStream(data))
				{
					var fileStreamDto = new FileStreamDto(memoryStream, fileName);
					SaveImageForUser(fileStreamDto, user.Username + "-Interest-WebPage-Image-" + interest.Interest.Name, imgFileNamesDto =>
						medium.AddImage(new ImageData(fileName, imgFileNamesDto.ThumbFilename, imgFileNamesDto.MediumFilename, imgFileNamesDto.FullFilename)));
				}
			}

			interest.AddMedium(medium);
		}

		public void SaveWebsite(User user, string siteUrl)
		{
			if (string.IsNullOrWhiteSpace(siteUrl)) throw new ArgumentNullException("siteUrl");

			siteUrl = siteUrl.FormatWebAddress();

			if (!UrlValidation.IsValidUrl(siteUrl)) throw new RulesException("SiteUrl", "Invalid URL");

			string html = _webContentService.GetHtmlFromUrl(siteUrl);

			string feedUrl = _feedFinder.GetFeedUrl(html);

			if (string.IsNullOrWhiteSpace(feedUrl))
			{
				feedUrl = null;
			}

			user.UpdateWebsite(siteUrl, feedUrl);
		}

		public void SavePassword(User user, string password)
		{
			string salt = PasswordHelper.GenerateSalt(16); //TODO: should this be 32 - encapsulate this somehwere
			string pHash = PasswordHelper.GetPasswordHash(password, salt);

			user.UpdatePassword(pHash, salt);
		}

		public void SaveLocation(User user, LocationData locationData)
		{
			MajorLocation majorLocation = _locationService.GetNearestMajorCity(locationData.Latitude, locationData.Longitude);

			user.UpdateLocation(locationData, majorLocation);
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

		public void RemoveMedium(User user, int mediumId)
		{
			Medium medium = user.GetMedium(mediumId);
			ImageFileNamesDto originalImageNames = GetImageFileNamesDto(medium);
			DeleteImages(originalImageNames);
			user.RemoveInterestMedium(medium);
		}

		public void RemoveInterest(User user, int interestId)
		{
			UserInterest interest = user.GetInterest(interestId);

			List<Medium> media = interest.Media.ToList();

			var files = new List<ImageFileNamesDto>(media.Count * 3);

			files.AddRange(media.Select(GetImageFileNamesDto));

			DeleteImages(files.ToArray());

			user.RemoveInterest(interest);

			_userInterestTasks.Delete(interest);
		}

		public void AddInterestToUser(User user, int interestId)
		{
			if(interestId < 1)
			{
				throw new RulesException("InterestId", "An existing interest must be passed in");
			}

			ProcessUserInterests(user, new[] { new UserInterestDto(interestId, null, null) });
		}

		public void AddInterestsToUser(string name, IEnumerable<UserInterestDto> interestDtos)
		{
			User user = GetUser(name);
			ProcessUserInterests(user, interestDtos);
		}

		public void SaveProfileImage(User user, FileStreamDto profileImage)
		{
			ImageFileNamesDto originalImageNames = null;
			if (user.ProfileImage != null)
			{
				originalImageNames = GetImageFileNamesDto(user.ProfileImage);
			}

			SaveImageForUser(profileImage, user.Username + "-Profile-Image", imgFileNamesDto =>
			{
				user.UpdateProfileImage(new ImageData(profileImage.FileName, imgFileNamesDto.ThumbFilename, imgFileNamesDto.MediumFilename, imgFileNamesDto.FullFilename));
				if (originalImageNames != null)
				{
					DeleteImages(originalImageNames);
				}
			});
		}

		public void StartConversation(string starter, string receiver, string messageText, string subject, string body)
		{
			//get the users involved in the conversation
			User sender = GetUser(starter),
				 recipient = GetUser(receiver);

			//check that both users exists
			//TODO: don't just return - throw ex when this kind of thing happens
			if (sender == null || recipient == null)
				return;

			//get the conversation that the recipient has with the sender, if any
			Conversation conversation = recipient.Conversations.SingleOrDefault(x => x.Users.Any(user => user.Username == starter));

			if (conversation == null)
			{
				//start a new conversation
				conversation = new Conversation(sender, recipient);
			}

			Message message = sender.WriteMessage(messageText);
			conversation.AddMessage(message);
			_conversationRepository.SaveOrUpdate(conversation);//TODO:remove this - it will be taken care of automatically
			_emailService.SendMail(recipient.Email, subject, body);
		}

		public Message ReplyToConversation(Conversation conversation, User sender, User receiver, string messageText, string subject, string body)
		{
			Message retVal = null;

			if (sender != null)
			{
				//TODO - don't just check or null - throw ex or don't check at all
				if (conversation != null)
				{
					retVal = sender.WriteMessage(messageText);

					conversation.AddMessage(retVal);

					_emailService.SendMail(receiver.Email, subject, body);
				}
			}
			return retVal;
		}

		public bool IsUsernameOrEmailAvailable(string value)
		{
			return _userRepository.IsUsernameOrEmailAvailable(value);
		}

		#endregion

		private static ImageFileNamesDto GetImageFileNamesDto(Medium medium)
		{
			var mediumWithImage = medium as IHasImage;

			if (mediumWithImage != null && mediumWithImage.ImageData != null)
			{
				return new ImageFileNamesDto
				{
					ThumbFilename = mediumWithImage.ImageData.ThumbFileName,
					MediumFilename = mediumWithImage.ImageData.MediumFileName,
					FullFilename = mediumWithImage.ImageData.FullFileName
				};
			}
			else
			{
				return null;
			}
		}

		private void SaveImageForUser(FileStreamDto profileImage, string metaPrefix, Action<ImageFileNamesDto> afterProcessing)
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
			//it's possible this could be called on media that doesn't have images 
			//associated with it
			//check .First() because a null passed to params has collection with 1 nul value
			if (imageNames != null && imageNames.Length > 0 && imageNames.First() != null)
			{
				var filesToDelete = new List<string>(imageNames.Length * 3);

				foreach (ImageFileNamesDto image in imageNames)
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

		private void ProcessUserInterests(User user, IEnumerable<UserInterestDto> interestDtos)
		{
			foreach (UserInterestDto interestDto in interestDtos)
			{
				Interest interest = interestDto.Id > 0 
					? _interestTasks.Get(interestDto.Id) 
					: _interestTasks.Create(interestDto.Name, interestDto.RelatedTo);

				var userInterest = user.AddInterest(interest, null);
				_userInterestTasks.SaveOrUpdate(userInterest);
			}
		}
	}
}