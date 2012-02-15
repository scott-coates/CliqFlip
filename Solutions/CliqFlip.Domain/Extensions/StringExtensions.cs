using System.Text.RegularExpressions;

namespace CliqFlip.Domain.Extensions
{
	//TODO: move this somewhere else..not really domain-specific
	public static class StringExtensions
	{
		private const string _alphaNumeric = "[^a-zA-Z0-9]";
		private const string _alphaNumericAndAllowed = "[^a-zA-Z0-9{0}]";

		public static string OnlyAlphaNumeric(this string input)
		{
			return Regex.Replace(input, _alphaNumeric, "", RegexOptions.Multiline);
		}

		public static string OnlyAlphaNumericAndAllowed(this string input, string allowed)
		{
			return Regex.Replace(input, string.Format(_alphaNumericAndAllowed, allowed), "", RegexOptions.Multiline);
		}

		public static string Slugify(this string phrase)
		{
			//algorithm found at http://stackoverflow.com/a/2921135/358007
			//removed the string length limitation

			string str = phrase.RemoveAccent().ToLower();
			// invalid chars           
			str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
			// convert multiple spaces into one space   
			str = Regex.Replace(str, @"\s+", " ").Trim();

			str = Regex.Replace(str, @"\s", "-"); // hyphens   
			return str;
		}

		private static string RemoveAccent(this string txt)
		{
			byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
			return System.Text.Encoding.ASCII.GetString(bytes);
		}
	}
}
