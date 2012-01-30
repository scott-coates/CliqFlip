using System.Web.Mvc;
using System.Web.Routing;

namespace CliqFlip.Web.Mvc
{
	public class RouteRegistrar
	{
		public static void RegisterRoutesTo(RouteCollection routes) 
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

			routes.MapRoute(
			 "Users",
			  "Users/{username}",
			  new { controller = "User", action = "Index"});

			routes.MapRoute(
				"Default",                                              // Route name
				"{controller}/{action}/{id}",                           // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional }); // Parameter defaults
		}
	}
}
