using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Data.Attributes;
using Microsoft.Practices.ServiceLocation;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class LikeController : ApiController
    {
        private readonly IPrincipal _principal;
        private readonly IUserTasks _userTasks;
        private readonly IPostTasks _postTasks;

        public LikeController() : this(ServiceLocator.Current.GetInstance<IPrincipal>(), ServiceLocator.Current.GetInstance<IUserTasks>(), ServiceLocator.Current.GetInstance<IPostTasks>())
        {
        }

        public LikeController(IPrincipal principal, IUserTasks userTasks, IPostTasks postTasks)
        {
            _principal = principal;
            _userTasks = userTasks;
            _postTasks = postTasks;
        }

        [HttpPost]
        [Transaction]
        public HttpResponseMessage Post(SaveLikeApiModel model)
        {
            var user = _userTasks.GetUser(_principal.Identity.Name);
            _postTasks.SaveLike(model.PostId, user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}