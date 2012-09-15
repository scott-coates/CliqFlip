using System.Web.Mvc;
using System.Web.Routing;
using CliqFlip.Common;

namespace CliqFlip.Web.Mvc
{
	public class RouteRegistrar
	{
		public static void RegisterRoutesTo(RouteCollection routes)
		{
			routes.IgnoreRoute("{*robotstxt}", new { robotstxt = @"(.*/)?robots.txt(/.*)?" });
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});

			//Error handling routes are in the MagicalUnicornErrorHandling.cs file

			routes.MapRoute(
				Constants.ROUTE_USER_HOME_PAGE,
				"u",
				new { controller = "User", action = "Home" });

			routes.MapRoute(
				"Users",
				"Users/{username}/{action}",
				new { controller = "User", action = "Index" });

			routes.MapRoute(
				"Login",
				"Login",
				new { controller = "User", action = "Login" });

			routes.MapRoute(
				"Invite",
				"Invite/{inviteKey}",
				new { controller = "Home", action = "Invite" });

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional }, 
				new[] { "CliqFlip.Web.Mvc.Controllers" }); // Parameter defaults
		}
	}
}