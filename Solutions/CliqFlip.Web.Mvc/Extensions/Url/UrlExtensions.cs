using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Web.Mvc;
using CliqFlip.Infrastructure.Common;
using CliqFlip.Web.Mvc.Extensions.Request;

namespace CliqFlip.Web.Mvc.Extensions.Url
{
	public static class UrlExtensions
	{
		public static string ContentWithVersioning(this UrlHelper helper, string fileName)
		{
			return helper.Content(AssemblyHelper.GeneratePath(fileName));
		}
	}
}