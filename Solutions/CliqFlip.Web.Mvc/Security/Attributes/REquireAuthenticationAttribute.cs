﻿using System;
using System.Web.Mvc;

namespace CliqFlip.Web.Mvc.Security.Attributes
{
	//http://blog.tomasjansson.com/2011/08/securing-your-asp-net-mvc-3-application/
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class RequireAuthenticationAttribute : AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
									filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
										typeof(AllowAnonymousAttribute), true);
			if (!skipAuthorization)
			{
				base.OnAuthorization(filterContext);
			}
		}

	}
}