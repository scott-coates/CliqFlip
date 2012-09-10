using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Http;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.Data.Attributes;
using Microsoft.Practices.ServiceLocation;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class SuggestedUserController : ApiController
    {
        private readonly IPrincipal _principal;
        private readonly IUserTasks _userTasks;

        public SuggestedUserController() : this(ServiceLocator.Current.GetInstance<IPrincipal>(), ServiceLocator.Current.GetInstance<IUserTasks>())
        {
        }

        public SuggestedUserController(IPrincipal principal, IUserTasks userTasks)
        {
            _principal = principal;
            _userTasks = userTasks;
        }

        [HttpGet]
        [Transaction]
        public IList<UserSearchResultDto> Get()
        {
            var user = _userTasks.GetUser(_principal.Identity.Name);
            return _userTasks.GetSuggestedUsers(user);
        }
    }
}