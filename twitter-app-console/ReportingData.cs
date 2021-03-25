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
            InitializeHttpClient();
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

        private void InitializeHttpClient() 
        {
            // TODO, Store this somewhere not in the code
           _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "AAAAAAAAAAAAAAAAAAAAALW4NwEAAAAAM3n5RgIWEQuAMATyE5ATSDBQOpg%3DmpIy6kCuC1BbysKMgcFvzeuJhDrxtivHTaMP5DqqXBjaSbSZsR");
        }
    }
}
