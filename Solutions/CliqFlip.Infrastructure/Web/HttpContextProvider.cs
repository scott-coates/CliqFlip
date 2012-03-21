using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using CliqFlip.Infrastructure.Web.Interfaces;

namespace CliqFlip.Infrastructure.Web
{
	public class HttpContextProvider : IHttpContextProvider
	{
		public HttpContextBase Context
		{
			get
			{
				return new HttpContextWrapper(HttpContext.Current);
			}
		}

		public HttpRequestBase Request
		{
			get
			{
				return Context.Request;
			}
		}

		public HttpResponseBase Response
		{
			get
			{
				return Context.Response;
			}
		}

		public HttpSessionStateBase Session
		{
			get
			{
				return Context.Session;
			}
		}

		public IPrincipal User
		{
			get
			{
				return Context.User;
			}
		}

		public IDictionary Items
		{
			get
			{
				return Context.Items;
			}
		}

		public NameValueCollection FormOrQuerystring
		{
			get
			{
				if (Request.RequestType == "POST")
				{
					return Request.Form;
				}
				return Request.QueryString;
			}
		}

		public HttpServerUtilityBase Server
		{
			get { return Context.Server; }
		}
	}
}
