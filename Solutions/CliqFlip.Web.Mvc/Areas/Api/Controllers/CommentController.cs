using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Domain.Dtos.Media;
using CliqFlip.Domain.Dtos.Post;
using CliqFlip.Web.Mvc.Areas.Api.Models.Feed;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.ViewModels.Search;
using Microsoft.Practices.ServiceLocation;
using SharpArch.NHibernate.Web.Mvc;
using CliqFlip.Infrastructure.Extensions;
using SharpArch.Web.Mvc.JsonNet;

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