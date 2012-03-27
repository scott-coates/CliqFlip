using System;

namespace CliqFlip.Web.Mvc.Security.Attributes
{
	//http://blog.tomasjansson.com/2011/08/securing-your-asp-net-mvc-3-application/
	//but i made a few changes
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class AllowAnonymousAttribute : Attribute
	{
	}
}