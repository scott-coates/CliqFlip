using System.Linq;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Filters;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class PostController : ApiController
    {
        private readonly IPrincipal _principal;
        private readonly IPostCollectionQuery _postCollectionQuery;

        public PostController() : this(ServiceLocator.Current.GetInstance<IPrincipal>(), ServiceLocator.Current.GetInstance<IPostCollectionQuery>())
        {
        }

        public PostController(IPrincipal principal, IPostCollectionQuery postCollectionQuery)
        {
            _principal = principal;
            _postCollectionQuery = postCollectionQuery;
        }

        // GET /api/feed
        [Queryable]
        public IQueryable<UserPostApiModel> Get(int? page)
        {
            return _postCollectionQuery.GetPostCollection(_principal.Identity.Name, page);
        }

        //// GET /api/feed/5
        //public string Get(int page)
        //{
        //    return "value";
        //}

        // POST /api/feed
        public void Post(string value)
        {
        }

        // PUT /api/feed/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/feed/5
        public void Delete(int id)
        {
        }
    }
}