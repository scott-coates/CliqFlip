using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CliqFlip.Domain.Extensions;

namespace CliqFlip.Domain.Search
{
	public static class FuzzySearch
	{
		public static int LevenshteinDistance(string src, string dest, bool scrub = true)
		{
			if (scrub)
			{
				src = src.ToLower().OnlyAlphaNumeric();
				dest = dest.ToLower().OnlyAlphaNumeric();
			}

			//http://www.codeproject.com/KB/recipes/fuzzysearch.aspx
			int[,] d = new int[src.Length + 1, dest.Length + 1];
			int i, j, cost;
			char[] str1 = src.ToCharArray();
			char[] str2 = dest.ToCharArray();

			for (i = 0; i <= str1.Length; i++)
			{
				d[i, 0] = i;
			}
			for (j = 0; j <= str2.Length; j++)
			{
				d[0, j] = j;
			}
			for (i = 1; i <= str1.Length; i++)
			{
				for (j = 1; j <= str2.Length; j++)
				{
					if (str1[i - 1] == str2[j - 1])
						cost = 0;
					else
						cost = 1;

					d[i, j] =
						Math.Min(
							d[i - 1, j] + 1, // Deletion
							Math.Min(
								d[i, j - 1] + 1, // Insertion
								d[i - 1, j - 1] + cost)); // Substitution

					if ((i > 1) && (j > 1) && (str1[i - 1] ==
											   str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
					{
						d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
					}
				}
			}

			return d[str1.Length, str2.Length];
		}

		public static T HighestRankedLevenshteinDistance<T>(IList<T> values, Func<T, string> getString, string target, int threshold) where T : class, new()
		{
			T retVal = null;

			var store = GetValuesWithinThresholdLevenshteinDistance(values, getString, target, threshold);

			if (store.Count > 0)
			{
				retVal = store.OrderBy(x => x.Value).First().Key; //the lower the better - that's why we order by asc
			}

			return retVal;
		}

		public static IDictionary<T, int> GetValuesWithinThresholdLevenshteinDistance<T>(IList<T> values, Func<T, string> getString, string target, int threshold) where T : class, new()
		{
			var retVal = new Dictionary<T, int>();
			int score;

			foreach (var val in values)
			{
				score = LevenshteinDistance(getString(val), target);
				if (score <= threshold)
				{
					retVal[val] = score;
				}
			}

			return retVal;
		}
	}
}
