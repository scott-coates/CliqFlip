using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Http;
using CliqFlip.Domain.Contracts.Tasks.Entities;
using CliqFlip.Domain.Dtos.User;
using CliqFlip.Messaging.Events.User;
using CliqFlip.Web.Mvc.Areas.Api.Queries.Interfaces;
using CliqFlip.Web.Mvc.Data.Attributes;
using MassTransit;
using Microsoft.Practices.ServiceLocation;

namespace CliqFlip.Web.Mvc.Areas.Api.Controllers
{
    [Authorize]
    public class SuggestedUserController : ApiController
    {
        private readonly IPrincipal _principal;
        private readonly IUserTasks _userTasks;
        private readonly IEndpoint _endpoint;

        public SuggestedUserController()
            : this(ServiceLocator.Current.GetInstance<IPrincipal>(), ServiceLocator.Current.GetInstance<IUserTasks>(), ServiceLocator.Current.GetInstance<IEndpoint>())
        {
        }

        public SuggestedUserController(IPrincipal principal, IUserTasks userTasks, IEndpoint endpoint)
        {
            _principal = principal;
            _userTasks = userTasks;
            _endpoint = endpoint;
        }

        [HttpGet]
        [Transaction]
        public IList<UserSearchResultDto> Get()
        {
            var username = _principal.Identity.Name;
            var user = _userTasks.GetUser(username);
            var userSearchResultDtos = _userTasks.GetSuggestedUsers(user);
            if (userSearchResultDtos.Count < 1)
            {
                _endpoint.Send(new UserRequestedSuggestedUsersEvent(username));
            }
            return userSearchResultDtos;
        }
    }
}