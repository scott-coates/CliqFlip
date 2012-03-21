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
using SharpArch.Web.Mvc.JsonNet;

namespace CliqFlip.Web.Mvc.Areas.Admin.Controllers
{
	[OutputCache(Location = OutputCacheLocation.None, NoStore = true)] //prevent caching
	public class ValidationController : Controller
	{
		private readonly ILocationService _locationService;
		private readonly IHttpContextProvider _httpContextProvider;

		public ValidationController(ILocationService locationService, IHttpContextProvider httpContextProvider)
		{
			_locationService = locationService;
			_httpContextProvider = httpContextProvider;
		}

		public ActionResult ZipCode(string zipCode)
		{
			object result = true;

			try
			{
				_httpContextProvider.Session[Constants.LOCATION_SESSION_KEY] = _locationService.GetLocation(zipCode);
			}
			catch (LocationException e)
			{
				_httpContextProvider.Session[Constants.LOCATION_SESSION_KEY] = null; //reset session if they change it
				result = e.Message;
			}

			return new JsonNetResult(result);
		}
	}
}