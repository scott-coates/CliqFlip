using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Http;
using CliqFlip.Web.Mvc.Areas.Api.Models.Post;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class PostController : ApiController
    {
        private readonly IPrincipal _principal;        
        private readonly IPostCollectionQuery _postCollectionQuery;


        public PostController(IPrincipal principal, IPostCollectionQuery postCollectionQuery)
        {
            _principal = principal;
            _postCollectionQuery = postCollectionQuery;
        }

        // GET /api/feed
        public PostCollectionApiModel Get(int? page)
        {
            return _postCollectionQuery.GetPostCollection(_principal.Identity.Name, page, Url);
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
