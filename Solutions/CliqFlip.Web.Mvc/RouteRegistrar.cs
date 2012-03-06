using System.Web.Mvc;
using System.Web.Routing;

namespace CliqFlip.Web.Mvc
{
	public class RouteRegistrar
	{
		public static void RegisterRoutesTo(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});

			//Error handling routes are in the MagicalUnicornErrorHandling.cs file

			routes.MapRoute(
				"Users",
				"Users/{username}/{action}",
				new {controller = "User", action = "Index"});

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new {controller = "Home", action = "Index", id = UrlParameter.Optional}); // Parameter defaults
		}
	}
}