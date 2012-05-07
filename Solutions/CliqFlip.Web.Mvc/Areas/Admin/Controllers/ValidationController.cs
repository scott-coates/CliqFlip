using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using CliqFlip.Domain.Common;
using CliqFlip.Infrastructure.Email.Interfaces;
using CliqFlip.Infrastructure.Exceptions;
using CliqFlip.Infrastructure.Location.Interfaces;
using CliqFlip.Infrastructure.Logging.Interfaces;
using CliqFlip.Infrastructure.Web.Interfaces;
using CliqFlip.Web.Mvc.Areas.Admin.ViewModels.Test;
using CliqFlip.Web.Mvc.Security.Attributes;
using SharpArch.Web.Mvc.JsonNet;
using CliqFlip.Domain.Contracts.Tasks;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[OutputCache(Location = OutputCacheLocation.None, NoStore = true)] //prevent caching
	[AllowAnonymous]
	public class ValidationController : Controller
	{
		private readonly ILocationService _locationService;
		private readonly IHttpContextProvider _httpContextProvider;
        private readonly IUserTasks _userTasks;

		public ValidationController(ILocationService locationService, IHttpContextProvider httpContextProvider, IUserTasks userTasks)
		{
			_locationService = locationService;
			_httpContextProvider = httpContextProvider;
            _userTasks = userTasks;
		}

		public ActionResult Location(string location)
		{
			object result = true;

			try
			{
				_httpContextProvider.Session[Constants.LOCATION_SESSION_KEY] = _locationService.GetLocation(location);
			}
			catch (LocationException e)
			{
				_httpContextProvider.Session[Constants.LOCATION_SESSION_KEY] = null; //reset session if they change it
				result = e.Message;
			}

			return new JsonNetResult(result);
		}

        public ActionResult Username(string username)
        {
            return new JsonNetResult(_userTasks.IsUsernameOrEmailAvailable(username));
        }

        public ActionResult Email(string email)
        {
            return new JsonNetResult(_userTasks.IsUsernameOrEmailAvailable(email));
        }
	}
}