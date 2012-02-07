using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Web.Mvc;
using CliqFlip.Web.Mvc.Extensions.Request;

namespace CliqFlip.Web.Mvc.Extensions.Url
{
	public static class UrlExtensions
	{
		public static bool IsCurrent(this UrlHelper urlHelper, string areaName)
		{
			return urlHelper.IsCurrent(areaName, null, null);
		}

		public static bool IsCurrent(this UrlHelper urlHelper, string areaName, string controllerName)
		{
			return urlHelper.IsCurrent(areaName, controllerName, null);
		}

		public static bool IsCurrent(this UrlHelper urlHelper, string areaName, string controllerName, params string[] actionNames)
		{
			return urlHelper.RequestContext.IsCurrentRoute(areaName, controllerName, actionNames);
		}

		public static string Selected(this UrlHelper urlHelper, string areaName)
		{
			return urlHelper.Selected(areaName, null, null);
		}

		public static string Selected(this UrlHelper urlHelper, string areaName, string controllerName)
		{
			return urlHelper.Selected(areaName, controllerName, null);
		}

		public static string Selected(this UrlHelper urlHelper, string areaName, string controllerName, params string[] actionNames)
		{
			return urlHelper.IsCurrent(areaName, controllerName, actionNames) ? "selected" : string.Empty;
		}
	}
 

}