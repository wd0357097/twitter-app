using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace twitter_data.Extensions
{
    public static class DataExtensions
    {
        /// <summary>
        /// calculates a percent
        /// </summary>
        /// <param name="count"></param>
        /// <param name="totalNumber"></param>
        /// <returns>double with 2 decimal places</returns>
        public static double CalculatePercentage(this int count, int totalNumber)
        {
            return Math.Round(Convert.ToDouble(count) / Convert.ToDouble(totalNumber) * 100, 2);
        }

        /// <summary>
        /// if regex matches add to dictionary object
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="regex"></param>
        /// <param name="searchText"></param>
        /// <returns>sorted dictionary</returns>
        public static Dictionary<string, int> RegexDataToDictionary(this Dictionary<string, int> dictionary, string regex, string searchText)
        {
            var dicRegex = new Regex(regex, RegexOptions.IgnoreCase);
            foreach (Match match in dicRegex.Matches(searchText))
            {
                if (match.Success)
                {
                    if (dictionary.ContainsKey(match.Value))
                    {
                        var value = dictionary.First(x => x.Key == match.Value).Value;
                        dictionary[match.Value] = value + 1;
                    }
                    else
                    {
                        dictionary.Add(match.Value, 1);
                    }
                }
            }
            if (dictionary.Count > 0)
            {
                dictionary = dictionary.OrderByDescending(kvp => kvp.Value).ToDictionary(x => x.Key, x => x.Value);
            }
            return dictionary;
        }

        public static Dictionary<string, int> OrderDictionary(this Dictionary<string, int> dictionary)
        {
            if (dictionary.Count > 0)
            {
                dictionary = dictionary.OrderByDescending(kvp => kvp.Value).ToDictionary(x => x.Key, x => x.Value);
            }
            return dictionary;
        }
    }
}
