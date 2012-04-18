using System;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Security.Attributes
{
    //http://stackoverflow.com/a/4901201 - found this nice solution and changed it a bit
    /// <summary>
    /// So we want to block old browsers.
    /// Any IE before IE9
    /// They flood the elmah error logs with JavaScript errors.
    /// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class BlockUnsupportedBrowsersAttribute : ActionFilterAttribute
	{
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var request = filterContext.HttpContext.Request;
            var browserName = request.Browser.Browser.Trim().ToUpperInvariant();
            var browserVersion = request.Browser.MajorVersion;
            if (browserName.Equals("IE") && browserVersion <= 8)
            {
                filterContext.Result = new ViewResult { ViewName = "BrowserNotSupported" };
            }
        }
	}
}