using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AttributeRouting.Web.Http;
using CliqFlip.Domain.Common;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Domain.Entities;
using CliqFlip.Domain.Exceptions;
using CliqFlip.Domain.ValueObjects;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Tasks.Commands.User;
using CliqFlip.Web.Mvc.Extensions.Exceptions;
using CliqFlip.Web.Mvc.Queries.Interfaces;
using CliqFlip.Web.Mvc.Security.Attributes;
using CliqFlip.Web.Mvc.ViewModels.Email;
using CliqFlip.Web.Mvc.ViewModels.Jeip;
using CliqFlip.Web.Mvc.ViewModels.User;
using CliqFlip.Web.Mvc.Views.Interfaces;
using Facebook;
using MassTransit;
using SharpArch.NHibernate.Web.Mvc;
using SharpArch.Web.Mvc.JsonNet;
using CliqFlip.Web.Mvc.Extensions.Controller;

namespace CliqFlip.Web.Mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IConversationQuery _conversationQuery;
        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IPrincipal _principal;
        private readonly IUserProfileQuery _userProfileQuery;
        private readonly IUserTasks _userTasks;
        private readonly IViewRenderer _viewRenderer;
        private readonly IServiceBus _serviceBus;

        public UserController(IUserTasks profileTasks, IUserProfileQuery userProfileQuery, IPrincipal principal, IConversationQuery conversationQuery, IHttpContextProvider httpContextProvider, IViewRenderer viewRenderer, IServiceBus serviceBus)
        {
            _userTasks = profileTasks;
            _userProfileQuery = userProfileQuery;
            _principal = principal;
            _conversationQuery = conversationQuery;
            _httpContextProvider = httpContextProvider;
            _viewRenderer = viewRenderer;
            _serviceBus = serviceBus;
        }

        [BlockUnsupportedBrowsers]
        [AllowAnonymous]
        public ActionResult Create()
        {
            if (_principal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User", new { username = _principal.Identity.Name });
            }
            return View(new UserCreateViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string accessToken)
        {
            var client = new FacebookClient(accessToken);

            dynamic result = client.Get("me", new { fields = "id" });

            string id = result.id;

            var user = _userTasks.GetUser(id);

            if (user == null)
            {
                var domainService = _serviceBus.GetEndpoint(new Uri("rabbitmq://localhost/Cliqflip.Service"));
                domainService.Send(new CreateNewUserCommand(id));
                return RedirectToAction("registration", new { accessToken });
            }
            return RedirectToRoute(Constants.ROUTE_USER_HOME_PAGE);
        }

        [AllowAnonymous]
        public ActionResult Registration(string accessToken)
        {
            var client = new FacebookClient(accessToken);

            dynamic result = client.Get("me", new { fields = "id,likes,location" });

            var likes = (JsonArray)result.likes["data"];

            string location = result.location.name;

            var likeNames = likes
                .Cast<dynamic>()
                .Select(x => x.name)
                .Cast<string>();

            var user = _userTasks.Create(result.id, location, likeNames);

            _userTasks.Login(user, true);

            return Content("success");
        }

        [AllowAnonymous]
        [HttpPost]
        [Transaction]
        public ActionResult Create(UserCreateViewModel profile)
        {
            if (ModelState.IsValid)
            {
                var profileToCreate = new UserCreateDto { Email = profile.Email, Password = profile.Password, Username = profile.Username };

                foreach (InterestCreate interest in profile.UserInterests)
                {
                    var userInterest = new UserAddInterestDto(interest.Id, interest.Name, interest.CategoryId);
                    profileToCreate.InterestDtos.Add(userInterest);
                }

                var locationData = GetLocationData();

                User user = _userTasks.Create_Old(profileToCreate, locationData);

                _userTasks.Login(user, false);

                return RedirectToAction("ThankYou");
            }

            //TODO: Implement PRG pattern for post forms
            return View(profile);
        }

        private LocationData GetLocationData()
        {
            var locationData = _httpContextProvider.Session[Constants.LOCATION_SESSION_KEY] as LocationData;

            if (locationData == null)
            {
                throw new Exception("The location data was not found in the user's session");
            }
            return locationData;
        }

        [Authorize]
        [Transaction]
        public ActionResult Account()
        {
            var viewModel = _userProfileQuery.GetUserAccount(_principal);
            return View("Account/Account", viewModel);
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult AccountEmail(UserAccountViewModel.UserAccountEmailViewModel userAccountEmailViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userTasks.GetUser(_principal.Identity.Name);
                user.UpdateEmail(userAccountEmailViewModel.Email);
                this.FlashSuccess("Email Updated");
                return RedirectToAction("Account");
            }

            //NOTE:we don't need to set the RouteData.Values["action"] like we do in the other failures
            //because Account() explicity sets the view name whereas the other
            //functions just call View();
            return Account();
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult AccountPassword(UserAccountViewModel.UserAccountPasswordViewModel userAccountPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userTasks.GetUser(_principal.Identity.Name);
                _userTasks.SavePassword(user, userAccountPasswordViewModel.Password);
                this.FlashSuccess("Password Updated");
                return RedirectToAction("Account");
            }

            return Account();
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult AccountLocation(UserAccountViewModel.UserAccountLocationViewModel userAccountLocationViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userTasks.GetUser(_principal.Identity.Name);
                var locationData = GetLocationData();
                _userTasks.SaveLocation(user, locationData);
                this.FlashSuccess("Location Updated");
                return RedirectToAction("Account");
            }

            return Account();
        }


        [Authorize]
        public ActionResult ThankYou()
        {
            var viewModel = new ThankYouViewModel { Username = _principal.Identity.Name };
            return View(viewModel);
        }

        [HttpPost]
        [Transaction]
        public ActionResult AddInterests(UserAddInterestsViewModel addInterestsViewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO: this should not be creating a userinterestdto - it's assigning the wrong id and is confusing
                IEnumerable<UserAddInterestDto> interestDtos = addInterestsViewModel.UserInterests.Select(x => new UserAddInterestDto(x.Id, x.Name, x.CategoryId));

                _userTasks.AddInterestsToUser(_principal.Identity.Name, interestDtos);
                return RedirectToAction("Interests", "User");
            }

            RouteData.Values["action"] = "Interests";
            //TODO: Implement PRG pattern for post forms
            return Interests(_principal.Identity.Name);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginAjax(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userTasks.Login(model.Username, model.Password, model.LogMeIn))
                {
                    return Content("Awesome! You are now logged in.");
                }
            }
            //TODO: Return a partial view instead of inline html
            return Content("<strong>Login failed!</strong><br/> Please verify your username and password.");
        }

        [BlockUnsupportedBrowsers]
        [AllowAnonymous]
        public ActionResult Login_Old()
        {
            if (_principal.Identity.IsAuthenticated)
            {
                return RedirectToRoute(Constants.ROUTE_USER_HOME_PAGE);
            }

            ViewBag.ReturnUrl = _httpContextProvider.Request.QueryString[Constants.RETURN_URL];

            return View(new UserLoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login_Old(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userTasks.Login(model.Username, model.Password, model.LogMeIn))
                {
                    //don't use formsAuth.Redirect as it sets the cookie again and will screw things up
                    //down the road

                    string returnUrl = _httpContextProvider.Request.QueryString[Constants.RETURN_URL];
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    {
                        return Redirect(FormsAuthentication.DefaultUrl);
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials");
                }
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            _userTasks.Logout(_principal.Identity.Name);
            return Redirect("~");
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveProfileImage(HttpPostedFileBase profileImage)
        {
            //http://haacked.com/archive/2010/07/16/uploading-files-with-aspnetmvc.aspx
            //model bound
            //TODO:put this into a viewmodel and check for is valid
            User user = _userTasks.GetUser(_principal.Identity.Name);
            if (profileImage == null)
            {
                ViewData.ModelState.AddModelError("Image", "You need to provide a file first... or don't. Have it your way.");
                RouteData.Values["action"] = "Index";
                return Index(_principal.Identity.Name);
            }
            else
            {
                try
                {
                    var fileStreamDto = new FileStreamDto(profileImage.InputStream, profileImage.FileName);
                    _userTasks.SaveProfileImage(user, fileStreamDto);
                }
                catch (RulesException rex)
                {
                    //TODO: Implement PRG pattern for post forms
                    //TODO: Log These exceptions in elmah
                    rex.AddModelStateErrors(ModelState);
                    RouteData.Values["action"] = "Index";
                    return Index(_principal.Identity.Name);
                }
                finally
                {
                    profileImage.InputStream.Dispose();
                }
            }

            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult CreatePostFromUrl(UserCreatePostFromUrlViewModel createPostFromUrlViewModel)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);

            if (ModelState.IsValid)
            {
                try
                {
                    _userTasks.PostImage(user, createPostFromUrlViewModel.UserInterestId, createPostFromUrlViewModel.PostDescription, createPostFromUrlViewModel.MediumUrl);
                }
                catch (RulesException e)
                {
                    //TODO: Implement PRG pattern for post forms
                    //TODO: Log These exceptions in elmah
                    e.AddModelStateErrors(ModelState);
                    RouteData.Values["action"] = "Interests";
                    return Interests(_principal.Identity.Name);
                }
                return RedirectToAction("Interests");
            }
            else
            {
                RouteData.Values["action"] = "Interests";
                return Interests(_principal.Identity.Name);
            }
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveInterestImage(UserSaveInterestImageViewModel userSaveInterestImageViewModel)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);
            if (userSaveInterestImageViewModel.InterestImage == null) //TODO: use required/not-null attribute instead of checking for null
            {
                ViewData.ModelState.AddModelError("Image", "You need to provide a file first... or don't. Have it your way.");
                RouteData.Values["action"] = "Interests";
                return Interests(_principal.Identity.Name);
            }
            else
            {
                try
                {
                    var fileStreamDto = new FileStreamDto(userSaveInterestImageViewModel.InterestImage.InputStream, userSaveInterestImageViewModel.InterestImage.FileName);
                    _userTasks.PostImage(user, fileStreamDto, userSaveInterestImageViewModel.UserInterestId, userSaveInterestImageViewModel.ImageDescription);
                }
                catch (RulesException rex)
                {
                    //TODO: Implement PRG pattern for post forms
                    //TODO: Log These exceptions in elmah
                    rex.AddModelStateErrors(ModelState);
                    RouteData.Values["action"] = "Interests";
                    return Interests(_principal.Identity.Name);
                }
                finally
                {
                    userSaveInterestImageViewModel.InterestImage.InputStream.Dispose();
                }
            }

            return RedirectToAction("Interests");
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveInterestVideo(UserSaveInterestVideoViewModel userSaveInterestImageViewModel)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);

            if (ModelState.IsValid)
            {
                try
                {
                    _userTasks.PostVideo(user, userSaveInterestImageViewModel.UserInterestId, userSaveInterestImageViewModel.VideoURL);
                }
                catch (RulesException e)
                {
                    //TODO: Implement PRG pattern for post forms
                    //TODO: Log These exceptions in elmah
                    e.AddModelStateErrors(ModelState);
                    RouteData.Values["action"] = "Interests";
                    return Interests(_principal.Identity.Name);
                }
                return RedirectToAction("Interests");
            }
            else
            {
                RouteData.Values["action"] = "Interests";
                return Interests(_principal.Identity.Name);
            }
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveInterestWebPage(UserSaveInterestWebPageViewModel userSaveInterestWebPageViewModel)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);

            if (ModelState.IsValid)
            {
                try
                {
                    _userTasks.PostWebPage(user, userSaveInterestWebPageViewModel.UserInterestId, userSaveInterestWebPageViewModel.LinkUrl);
                }
                catch (RulesException e)
                {
                    //TODO: Implement PRG pattern for post forms
                    //TODO: Log These exceptions in elmah
                    e.AddModelStateErrors(ModelState);
                    RouteData.Values["action"] = "Interests";
                    return Interests(_principal.Identity.Name);
                }
                return RedirectToAction("Interests");
            }
            else
            {
                RouteData.Values["action"] = "Interests";
                return Interests(_principal.Identity.Name);
            }
        }

        [Authorize]
        [Transaction]
        public ActionResult MakePostDefault(int postId)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);

            user.MakePostDefault(postId);

            return RedirectToAction("Interests");
        }

        [Authorize]
        [Transaction]
        public ActionResult RemovePost(int postId)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);

            _userTasks.RemovePost(user, postId);

            return RedirectToAction("Interests");
        }

        [Authorize]
        [Transaction]
        public ActionResult RemoveInterest(int interestId)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);

            _userTasks.RemoveInterest(user, interestId);

            return RedirectToAction("Interests");
        }

        //TODO: Can we remove this. I don't see it being used.
        [Authorize]
        [Transaction]
        public ActionResult AddSingleInterest(int interestId)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);

            _userTasks.AddInterestToUser(user, interestId);

            return RedirectToAction("Interests");
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveMindMap(UserSaveMindMapViewModel userSaveMindMapViewModel)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);
            user.UpdateInterest(userSaveMindMapViewModel.Id, new UserInterestOption(userSaveMindMapViewModel.Passion, userSaveMindMapViewModel.XAxis, userSaveMindMapViewModel.YAxis));

            return new EmptyResult();
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveHeadline(JeipSaveTextViewModel saveTextViewModel)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);
            user.UpdateHeadline(saveTextViewModel.New_Value);
            var retVal = new JeipSaveResponseViewModel { html = saveTextViewModel.New_Value, is_error = false };
            return new JsonNetResult(retVal);
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveBio(JeipSaveTextViewModel saveTextViewModel)
        {
            //get conversation and save it
            User user = _userTasks.GetUser(_principal.Identity.Name);
            user.UpdateBio(saveTextViewModel.New_Value);
            var retVal = new JeipSaveResponseViewModel { html = saveTextViewModel.New_Value, is_error = false };
            return new JsonNetResult(retVal);
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveTwitterUsername(JeipSaveTextViewModel saveTextViewModel)
        {
            //get conversation and save it
            User user = _userTasks.GetUser(_principal.Identity.Name);
            user.UpdateTwitterUsername(saveTextViewModel.New_Value);
            var retVal = new JeipSaveResponseViewModel { html = saveTextViewModel.New_Value, is_error = false };
            return new JsonNetResult(retVal);
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveYouTubeUsername(JeipSaveTextViewModel saveTextViewModel)
        {
            //get conversation and save it
            User user = _userTasks.GetUser(_principal.Identity.Name);
            user.UpdateYouTubeUsername(saveTextViewModel.New_Value);
            var retVal = new JeipSaveResponseViewModel { html = saveTextViewModel.New_Value, is_error = false };
            return new JsonNetResult(retVal);
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveWebiste(JeipSaveTextViewModel saveTextViewModel)
        {
            //get conversation and save it
            User user = _userTasks.GetUser(_principal.Identity.Name);
            var retVal = new JeipSaveResponseViewModel();

            try
            {
                _userTasks.SaveWebsite(user, saveTextViewModel.New_Value);
                retVal.is_error = false;
                retVal.html = user.UserWebsite.SiteUrl;
            }
            catch (RulesException rex)
            {
                retVal.is_error = true;
                retVal.error_text = rex.Errors.First().ErrorMessage;
            }

            return new JsonNetResult(retVal);
        }

        [Authorize]
        [HttpPost]
        [Transaction]
        public ActionResult SaveFacebook(string fbid)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);
            user.UpdateFacebookUsername(fbid);
            return new JsonNetResult();
        }


        [BlockUnsupportedBrowsers]
        [Transaction]
        public ActionResult Index(string username)
        {
            UserProfileIndexViewModel user = _userProfileQuery.GetUserProfileIndex(username, _principal);
            if (user != null)
            {
                user.SaveHeadlineUrl = "\"" + Url.Action("SaveHeadline", "User") + "\"";
                user.SaveMindMapUrl = "\"" + Url.Action("SaveMindMap", "User") + "\"";
                user.SaveBioUrl = "\"" + Url.Action("SaveBio", "User") + "\"";
                user.SaveTwitterUsernameUrl = "\"" + Url.Action("SaveTwitterUsername", "User") + "\"";
                user.SaveYouTubeUsernameUrl = "\"" + Url.Action("SaveYouTubeUsername", "User") + "\"";
                user.SaveWebsiteUrl = "\"" + Url.Action("SaveWebiste", "User") + "\"";
                user.SocialPageUrl = "\"" + Url.Action("SocialMedia", "User") + "\"";
                return View(user);
            }

            //http://stackoverflow.com/a/4985562/173957
            throw new HttpException((int)HttpStatusCode.NotFound, "Not found");
        }

        [BlockUnsupportedBrowsers]
        [Transaction]
        public ActionResult SocialMedia(string username)
        {
            UserSocialMediaViewModel user = _userProfileQuery.GetUserSocialMedia(username, _principal);
            return View(user);
        }

        [BlockUnsupportedBrowsers]
        [Transaction]
        public ActionResult Interests(string username)
        {
            UserInterestsViewModel user = _userProfileQuery.GetUserIntersets(username, _principal);
            user.MakeDefaultUrl = "\"" + Url.Action("MakePostDefault", "User") + "\"";
            user.RemovePostUrl = "\"" + Url.Action("RemovePost", "User") + "\"";

            return View(user);
        }


        [Transaction]
        [Authorize]
        public ActionResult StartConversationWith()
        {
            return PartialView();
        }

        [Transaction]
        [Authorize]
        [HttpPost]
        public ActionResult StartConversationWith(StartConversationWithViewModel model)
        {
            if (ModelState.IsValid)
            {
                string body = _viewRenderer.RenderView(this, "~/Views/Email/NewConversation.cshtml"
                                                       , new NewConversationViewModel
                                                       {
                                                           ToUsername = model.Username,
                                                           FromUsername = _principal.Identity.Name
                                                       });

                _userTasks.StartConversation(_principal.Identity.Name
                                             , model.Username
                                             , model.Text
                                             , "New Message on CliqFlip"
                                             , body);

                return Json(new { success = true });
            }
            //TODO - throw ex or display to user the validation error
            return Json(new { success = false });
        }

        [Transaction]
        [Authorize]
        public ActionResult ReplyToConversation(int id)
        {
            var model = new UserReplyToConversationViewModel { Id = id };
            return PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [Transaction]
        public ActionResult ReplyToConversation(UserReplyToConversationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User sender = _userTasks.GetUser(_principal.Identity.Name);
                Conversation conversation = sender.Conversations.SingleOrDefault(x => x.Id == model.Id);
                List<User> users = conversation.Users.ToList();

                users.Remove(sender);
                User reveiver = users.Single();

                string subject = _principal.Identity.Name + " has Replied to You";

                string body = _viewRenderer.RenderView(this, "~/Views/Email/ReplyToConversation.cshtml"
                                                       , new ReplyToViewModel
                                                       {
                                                           ToUsername = reveiver.Username,
                                                           FromUsername = _principal.Identity.Name
                                                       });

                Message message = _userTasks
                    .ReplyToConversation(conversation
                                         , sender
                                         , reveiver
                                         , model.Text
                                         , subject
                                         , body);

                return PartialView("Message", new MessageViewModel(message));
            }

            //TODO - throw ex or display to user the validation error
            return new EmptyResult();
        }

        [BlockUnsupportedBrowsers]
        [Authorize]
        [Transaction]
        public ActionResult Inbox()
        {
            UserInboxViewModel model = _userProfileQuery.GetUserInbox(_principal);
            return View(model);
        }

        [Authorize]
        [Transaction]
        public int NewMessageCount()
        {
            return _userTasks.GetUser(_principal.Identity.Name).GetNumberOfUnreadConversations();
        }

        [BlockUnsupportedBrowsers]
        [Authorize]
        [Transaction]
        public ActionResult Home()
        {
            string username = _principal.Identity.Name;

            var user = _userTasks.GetUser(username);
            return new JsonNetResult(new { user.Username, Interests = user.Interests.Select(y => y.Interest.Name) });
        }

        [Authorize]
        [Transaction]
        [BlockUnsupportedBrowsers]
        public ActionResult Flip()
        {
            string username = _principal.Identity.Name;
            User nextUser = _userTasks.GetSuggestedUser(username);

            return RedirectToAction("Index", new { username = nextUser.Username });
        }

        [Authorize]
        [Transaction]
        public ActionResult ReadConversation(int id)
        {
            User user = _userTasks.GetUser(_principal.Identity.Name);
            user.ReadConversation(id);
            IEnumerable<MessageViewModel> messages = _conversationQuery.GetMessages(id, _principal.Identity.Name);
            return PartialView(messages);
        }
    }
}