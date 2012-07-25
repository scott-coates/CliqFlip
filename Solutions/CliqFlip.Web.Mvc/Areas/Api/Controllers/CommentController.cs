using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Data.Attributes;
using Microsoft.Practices.ServiceLocation;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class CommentController : ApiController
    {
        private readonly IPrincipal _principal;
        private readonly IUserTasks _userTasks;
        private readonly IPostTasks _postTasks;

        public CommentController() : this(ServiceLocator.Current.GetInstance<IPrincipal>(), ServiceLocator.Current.GetInstance<IUserTasks>(), ServiceLocator.Current.GetInstance<IPostTasks>())
        {
        }

        public CommentController(IPrincipal principal, IUserTasks userTasks, IPostTasks postTasks)
        {
            _principal = principal;
            _userTasks = userTasks;
            _postTasks = postTasks;
        }

        [Transaction]
        [HttpPost]
        public HttpResponseMessage Post(CommentApiModel comment)
        {
            var user = _userTasks.GetUser(_principal.Identity.Name);
            _postTasks.SaveComment(new SavePostCommentDto { CommentText = comment.CommentText, PostId = comment.PostId }, user);
            return Request.CreateResponse(HttpStatusCode.OK, new { ProfileImageUrl = user.ProfileImage.ImageData.ThumbFileName, user.Username });
        }
    }
}