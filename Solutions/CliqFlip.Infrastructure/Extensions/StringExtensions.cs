using System.Text.RegularExpressions;

namespace CliqFlip.Infrastructure.Extensions
{
	public static class StringExtensions
	{
		public static string FormatWebAddress(this string input)
		{
			const string httpPrefix = "http://";

			if(!input.ToLower().StartsWith(httpPrefix))
			{
				input = httpPrefix + input;
			}

			return input;
		}
	}
}
