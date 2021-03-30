using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using twitter_data.Entities;
using twitter_data.Interface;

namespace twitter_data.Managers
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
            try
            {
                // reporting data
                var data = new ReportingData
                {
                    DateStartTime = DateTime.Now.TimeOfDay,
                };
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _client.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead);
                var body = await response.Content.ReadAsStreamAsync();
                var reader = new StreamReader(body);

                while (!reader.EndOfStream)
                {
                    var tweet = reader.ReadLine();
                    var currentTweet = JsonConvert.DeserializeObject<TwitterResponse>(tweet);
                    // make sure the current tweet is not null
                    if (currentTweet != null)
                    {
                        data.CurrentTweet = currentTweet;
                        OnProcessCompleted(data);// notify
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Unable to process stream: {ex.Message}");
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
