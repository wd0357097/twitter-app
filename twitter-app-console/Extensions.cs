using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace twitter_app_console
{
    public static class Extensions
    {
        public static string ToContentString(this HttpContent content)
        {
            Task<string> value = GetStringResult(content);
            return value.Result;
        }
        private static async Task<string> GetStringResult(HttpContent content)
        {
            var value = await content.ReadAsStringAsync();
            return value;
        }
    }
}
