using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CliqFlip.Infrastructure.Logging;

namespace CliqFlip.Web.Mvc.Security.Attributes
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class FormsAuthReadUserDataAttribute : AuthorizeAttribute
	{
		//http://stackoverflow.com/a/5947309/173957
		//http://stackoverflow.com/questions/8984085/httpcontext-current-user-isinrole-not-working
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			var isAuthorized = base.AuthorizeCore(httpContext);
			if (isAuthorized)
			{
				var identity = (FormsIdentity)HttpContext.Current.User.Identity;

				FormsAuthenticationTicket ticket = identity.Ticket;

				var principal = new GenericPrincipal(identity, new[] { ticket.UserData });
				new ElmahLogger().LogException(new Exception("principal " + ticket.UserData));
				httpContext.User = principal;
			}

			new ElmahLogger().LogException(new Exception("is authorized " + isAuthorized));
			return isAuthorized;
		}

	}
}