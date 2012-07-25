using System.Security.Principal;
using System.Web.Http;
using CliqFlip.Domain.Contracts.Tasks;
using CliqFlip.Web.Mvc.Areas.Api.Models.Feed;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.Data.Attributes;
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

        [HttpGet]
        [Transaction]
        public FeedListApiModel Get(int? page, string q)
        {
            return _feedListQuery.GetFeedList(_principal.Identity.Name, page, q);
        }
    }
}