using System.Web.Routing;
using System.Linq;

namespace CliqFlip.Web.Mvc.Extensions.Request
{
	public static class RequestExtensions
	{
		public static bool IsCurrentRoute(this RequestContext context, string areaName)
		{
			return context.IsCurrentRoute(areaName, null, null);
		}

		public static bool IsCurrentRoute(this RequestContext context, string areaName, string controllerName)
		{
			return context.IsCurrentRoute(areaName, controllerName, null);
		}

		public static bool IsCurrentRoute(this RequestContext context, string areaName, string controllerName, params string[] actionNames)
		{
			RouteData routeData = context.RouteData;
			var routeArea = routeData.DataTokens["area"] as string;
			bool current = ((string.IsNullOrEmpty(routeArea) && string.IsNullOrEmpty(areaName)) || (routeArea == areaName)) &&
			               ((string.IsNullOrEmpty(controllerName)) || (routeData.GetRequiredString("controller") == controllerName)) &&
			               ((actionNames == null) || actionNames.Contains(routeData.GetRequiredString("action")));

			return current;
		}
	}
}