using System.Security.Principal;
using System.Web.Http;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class FeedController : ApiController
    {
        private readonly IPrincipal _principal;
        private readonly IFeedListQuery _feedListQuery;

        public FeedController() : this(ServiceLocator.Current.GetInstance<IPrincipal>(), ServiceLocator.Current.GetInstance<IFeedListQuery>())
        {
        }

        public FeedController(IPrincipal principal, IFeedListQuery feedListQuery)
        {
            _principal = principal;
            _feedListQuery = feedListQuery;
        }

        // GET /api/feed
        public dynamic Get(int? page)
        {
            return _feedListQuery.GetFeedList(_principal.Identity.Name, page);
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