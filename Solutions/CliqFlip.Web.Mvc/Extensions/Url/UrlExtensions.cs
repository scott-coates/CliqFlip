using System;
using System.Web;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Common;

namespace CliqFlip.Web.Mvc.Extensions.Url
{
	public static class UrlExtensions
	{
		public static string ContentWithVersioning(this UrlHelper helper, string fileName)
		{
			return helper.Content(AssemblyHelper.GeneratePath(fileName));
		}

		public static string ToPublicUrl(this UrlHelper urlHelper, string relativeUriString)
		{
			//http://support.appharbor.com/kb/getting-started/workaround-for-generating-absolute-urls-without-port-number
			HttpContextBase httpContext = urlHelper.RequestContext.HttpContext;

			var uriBuilder = new UriBuilder
			{
				Host = httpContext.Request.Url.Host,
				Path = "/",
				Port = 80,
				Scheme = "http",
			};

			if (httpContext.Request.IsLocal)
			{
				uriBuilder.Port = httpContext.Request.Url.Port;
			}

			return new Uri(uriBuilder.Uri, relativeUriString).AbsoluteUri;
		}
	}
}