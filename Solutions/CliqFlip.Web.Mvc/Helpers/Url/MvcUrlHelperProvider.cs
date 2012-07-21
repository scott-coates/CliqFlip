using System.Web;
using System.Web.Mvc;
using CliqFlip.Web.Mvc.Helpers.Url.Interfaces;

namespace CliqFlip.Web.Mvc.Helpers.Url
{
    public class MvcUrlHelperProvider : IMvcUrlHelperProvider
    {
        public UrlHelper ProvideUrlHelper()
        {
            //http://stackoverflow.com/questions/2031995/call-urlhelper-in-models-in-asp-net-mvc
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            return url;
        }
    }
}