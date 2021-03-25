using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace twitter_app_console
{
    public class ReportingData : IReportingData
    {
        private readonly HttpClient _client;

        public ReportingData() 
        {
            _client = new HttpClient();
        }

        public ReportingData(HttpClient client)
        {
            _client = client;
        }

        public int AverageNumberOfTweets(TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public decimal PercentOfTweetsThatContainsEmojis()
        {
            throw new NotImplementedException();
        }

        public string TopDomainsOfUrlsInTweets()
        {
            throw new NotImplementedException();
        }

        public string TopEmojisInTweets(int number)
        {
            throw new NotImplementedException();
        }

        public string TopHashTags()
        {
            throw new NotImplementedException();
        }

        public int TotalNumberOfTweets()
        {
            throw new NotImplementedException();
        }

        public decimal TweetsThatContainPhotoUrl()
        {
            throw new NotImplementedException();
        }

        public decimal TweetsThatContainUrl()
        {
            throw new NotImplementedException();
        }
    }
}
