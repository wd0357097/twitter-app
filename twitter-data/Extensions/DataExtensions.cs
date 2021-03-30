using System;
using System.Collections.Generic;
using System.Linq;

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
        /// orders the dictionary 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns>sorted dictionary</returns>
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
