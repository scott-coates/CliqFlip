using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CliqFlip.Common.Extensions
{
    public static class StringExtensions
    {
        private const string _alphaNumeric = "[^a-zA-Z0-9]";
        private const string _alphaNumericAndAllowed = "[^a-zA-Z0-9{0}]";

        public static string FormatWebAddress(this string input)
        {
            const string httpPrefix = "http://";

            if (!input.ToLower().StartsWith(httpPrefix))
            {
                input = httpPrefix + input;
            }

            return input;
        }

        public static Stream ConvertImageDataToStream(this string input)
        {
            string removeDataPrefix = input.Substring(input.IndexOf(",", StringComparison.Ordinal) + 1);
            byte[] bytearray = Convert.FromBase64String(removeDataPrefix);
            return new MemoryStream(bytearray);
        }

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
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}