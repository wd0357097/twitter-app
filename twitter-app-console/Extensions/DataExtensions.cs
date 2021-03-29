using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace twitter_app_console.Extensions
{
    public static class DataExtensions
    {
        public static double CalculatePercentage(this int count, int totalNumber)
        {
            return Math.Round(Convert.ToDouble(count) / Convert.ToDouble(totalNumber) * 100, 2);
        }

        // TODO, add change to look for all matches in a string
        public static Dictionary<string, int> RegexDataToDictionary(this Dictionary<string, int> dictionary, string regex, string searchText)
        {
            var match = Regex.Match(searchText, regex, RegexOptions.IgnoreCase);
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
            if (dictionary.Count > 0)
            {
                dictionary = dictionary.OrderByDescending(kvp => kvp.Value).ToDictionary(x => x.Key, x => x.Value);
            }
            return dictionary;
        }
    }
}
