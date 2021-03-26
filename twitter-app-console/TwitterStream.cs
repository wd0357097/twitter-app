using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

namespace twitter_app_console
{
    public class TwitterStream : IAppStream
    {
        private HttpClient _client;

        public TwitterStream()
        {
            InitializeHttpClient();
        }

        public TwitterStream(HttpClient client)
        {
            _client = client;
        }


        public event EventHandler<ReportingData> ReportingData;
        public async Task StartStreamAsync(string url)
        {
            int numberOfTweets = 0;
            var data = new ReportingData();
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
                data.TotalNumberOfTweets = numberOfTweets;
                OnProcessCompleted(data);// notify
            }
        }

        private void OnProcessCompleted(ReportingData e)
        {
            ReportingData?.Invoke(this, e);
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
