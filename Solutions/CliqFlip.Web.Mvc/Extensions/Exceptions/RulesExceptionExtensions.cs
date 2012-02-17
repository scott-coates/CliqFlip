using System;
using System.Linq;
using System.Web.Mvc;
using CliqFlip.Domain.Exceptions;

namespace CliqFlip.Web.Mvc.Extensions.Exceptions
{
	public static class RulesExceptionExtensions
	{
		public static void AddModelStateErrors(this RulesException ex, ModelStateDictionary modelState, string prefix = null)
		{
			AddModelStateErrors(ex, modelState, x => true, prefix);
		}

		public static void AddModelStateErrors(this RulesException ex, ModelStateDictionary modelState, Func<ErrorInfo, bool> errorFilter, string prefix = null)
		{
			if (errorFilter == null) throw new ArgumentNullException("errorFilter");
			prefix = prefix == null ? "" : prefix + ".";
			foreach (ErrorInfo errorInfo in ex.Errors.Where(errorFilter))
			{
				string key = prefix + errorInfo.PropertyName;
				modelState.AddModelError(key, errorInfo.ErrorMessage);

				// Workaround for http://xval.codeplex.com/WorkItem/View.aspx?WorkItemId=1297 (ASP.NET MVC bug)
				// Ensure that some value object is registered in ModelState under this key
				ModelState existingModelStateValue;
				if (modelState.TryGetValue(key, out existingModelStateValue) && existingModelStateValue.Value == null)
				{
					existingModelStateValue.Value = new ValueProviderResult(null, null, null);
				}
			}
		}
	}
}