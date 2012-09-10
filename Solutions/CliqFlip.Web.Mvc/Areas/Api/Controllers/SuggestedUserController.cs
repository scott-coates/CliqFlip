using System.Security.Principal;
using System.Web.Http;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.Data.Attributes;
using Microsoft.Practices.ServiceLocation;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class SuggestedUserController : ApiController
    {
        private readonly IPrincipal _principal;
        private readonly IFeedListQuery _feedListQuery;

        public SuggestedUserController() : this(ServiceLocator.Current.GetInstance<IPrincipal>(), ServiceLocator.Current.GetInstance<IFeedListQuery>())
        {
        }

        public SuggestedUserController(IPrincipal principal, IFeedListQuery feedListQuery)
        {
            _principal = principal;
            _feedListQuery = feedListQuery;
        }

        [HttpGet]
        [Transaction]
        public string Get()
        {
            return "HI";
        }
    }
}