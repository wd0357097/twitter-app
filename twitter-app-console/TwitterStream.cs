using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

namespace twitter_app_console
{
    public class TwitterStream : IAppStream
    {
        private HttpClient _client;
        private int numberOfTweets;

        public event EventHandler<IReportingData> ReportingData;

        public TwitterStream() 
        {
            InitializeHttpClient();
        }

        public TwitterStream(HttpClient client)
        {
            _client = client;
        }

        public async Task StartStreamAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead);
            var body = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(body);
            while (!reader.EndOfStream)
            {
                numberOfTweets++;
                var tweet = reader.ReadLine();
                var tweetObject = JsonConvert.DeserializeObject<TwitterResponse>(tweet);
                //Console.WriteLine(tweet);
                //Console.WriteLine("Number Of Tweets: " + this.TotalNumberOfTweets);
            }
        }

        public int TotalNumberOfTweets => numberOfTweets;

        public decimal PercentOfTweetsThatContainsEmojis => throw new NotImplementedException();

        public string TopHashTags => throw new NotImplementedException();

        public decimal TweetsThatContainUrl => throw new NotImplementedException();

        public decimal TweetsThatContainPhotoUrl => throw new NotImplementedException();

        public string TopDomainsOfUrlsInTweets => throw new NotImplementedException();

        public int AverageNumberOfTweets(TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public string TopEmojisInTweets(int number)
        {
            throw new NotImplementedException();
        }

        private void InitializeHttpClient()
        {
            _client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite)
            };
            _client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["token"]);
        }
    }
}
