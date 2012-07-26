using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using AttributeRouting.Web.Http;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.Data.Attributes;
using Microsoft.Practices.ServiceLocation;
using CliqFlip.Infrastructure.Extensions;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class PostController : ApiController
    {
        private readonly IPrincipal _principal;
        private readonly IUserTasks _userTasks;
        private readonly IPostOverviewQuery _postOverviewQuery;
        private readonly IPostTasks _postTasks;

        public PostController()
            : this(ServiceLocator.Current.GetInstance<IPrincipal>(), ServiceLocator.Current.GetInstance<IUserTasks>(), ServiceLocator.Current.GetInstance<IPostOverviewQuery>(), ServiceLocator.Current.GetInstance<IPostTasks>())
        {
        }

        public PostController(IPrincipal principal, IUserTasks userTasks, IPostOverviewQuery postOverviewQuery, IPostTasks postTasks)
        {
            _principal = principal;
            _userTasks = userTasks;
            _postOverviewQuery = postOverviewQuery;
            _postTasks = postTasks;
        }

        [HttpPost]
        [Transaction]
        public HttpResponseMessage Post(SavePostApiModel model)
        {
            var user = _userTasks.GetUser(_principal.Identity.Name);

            var mediumType = model.PostType.ToLower();

            switch (mediumType)
            {
                case "photo":
                    if (string.IsNullOrEmpty(model.FileName))
                    {
                        _userTasks.PostImage(user, model.InterestId, model.Description, model.PostData);
                    }
                    else
                    {
                        using (var memoryStream = model.PostData.ConvertImageDataToStream())
                        {
                            _userTasks.PostImage(
                                user,
                                new FileStreamDto(memoryStream, model.FileName),
                                model.InterestId,
                                model.Description);
                        }
                    }
                    break;
                case "video":
                    _userTasks.PostVideo(user, model.InterestId, model.PostData);
                    break;
                case "link":
                    _userTasks.PostWebPage(user, model.InterestId, model.PostData);
                    break;
                case "status":
                    _userTasks.PostStatus(user, model.InterestId, model.Description);
                    break;
                default:
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.BadRequest, "A medium type is required"));
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        [HttpGet]
        [Transaction]
        public PostOverviewApiModel Get(int id)
        {
            var viewModel = _postOverviewQuery.GetPostOverview(id, _userTasks.GetUser(_principal.Identity.Name));
            return viewModel;
        }


        [POST("/api/post/{id}/like")]
        [Transaction]
        public HttpResponseMessage Like(SaveLikeApiModel model)
        {
            var user = _userTasks.GetUser(_principal.Identity.Name);
            _postTasks.SaveLike(model.PostId, user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Transaction]
        [HttpPost]
        public HttpResponseMessage Comment(CommentApiModel comment)
        {
            var user = _userTasks.GetUser(_principal.Identity.Name);
            _postTasks.SaveComment(new SavePostCommentDto { CommentText = comment.CommentText, PostId = comment.PostId }, user);
            return Request.CreateResponse(HttpStatusCode.OK, new { ProfileImageUrl = user.ProfileImage.ImageData.ThumbFileName, user.Username });
        }
    }
}