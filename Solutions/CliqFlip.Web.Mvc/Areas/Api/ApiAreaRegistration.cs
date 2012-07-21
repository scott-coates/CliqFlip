using System.Web.Http;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Areas.Api 
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            //context.Routes.MapHttpRoute(
            //    "Api_default",
            //    "Api/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional } ,
            //    new[] { "CliqFlip.Web.Mvc.Areas.Api.Controllers" });
        }
    }
}
