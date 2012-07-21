using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Helpers.Url.Interfaces
{
    public interface IMvcUrlHelperProvider
    {
        UrlHelper ProvideUrlHelper();
    }
}