using Microsoft.Web.Mvc;

namespace CliqFlip.Infrastructure.Validation
{
	public static class UrlValidation
	{
		public static bool IsValidUrl(string url)
		{
			return new UrlAttribute().IsValid(url);
		}
	}
}