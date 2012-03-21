using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace CliqFlip.Infrastructure.Web.Interfaces
{
	public interface IHttpContextProvider
	{
		HttpContextBase Context { get; }
		HttpRequestBase Request { get; }
		HttpResponseBase Response { get; }
		HttpSessionStateBase Session { get; }
		IPrincipal User { get; }
		IDictionary Items { get; }
		HttpServerUtilityBase Server { get; }
		NameValueCollection FormOrQuerystring { get; }
	}
}
