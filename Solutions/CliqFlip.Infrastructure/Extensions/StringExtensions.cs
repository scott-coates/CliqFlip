using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CliqFlip.Infrastructure.Extensions
{
    public static class StringExtensions
    {
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
            var removeDataPrefix = input.Substring(input.IndexOf(",", StringComparison.Ordinal) + 1);
            var bytearray = Convert.FromBase64String(removeDataPrefix);
            return new MemoryStream(bytearray);
        }
    }
}