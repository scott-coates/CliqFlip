using System;
using System.Collections.Generic;
using System.Linq;

namespace CliqFlip.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            var rnd = new Random();
            return source.OrderBy(item => rnd.Next());
        }

        public static IEnumerable<T> TakeFeedPage<T>(this IEnumerable<T> input, int? page)
        {
            return input.Skip(((page ?? 1) - 1) * Constants.FEED_LIMIT).Take(Constants.FEED_LIMIT);
        }
    }
}