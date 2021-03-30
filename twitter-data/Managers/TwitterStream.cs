using System;
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
    /// <summary>
    /// uses to gather data from the twitter sample stream
    /// </summary>
    public class TwitterStream : IAppStream
    {
        private HttpClient _client;

        public TwitterStream(string authToken)
        {
            InitializeHttpClient(authToken);
        }

        public TwitterStream(HttpClient client)
        {
            _client = client;
        }

        public event EventHandler<ReportingData> ReportingData;

        /// <summary>
        /// starts a continues stream until cancellation token is sent in
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartStreamAsync(string url, CancellationToken cancellationToken)
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
                using var reader = new StreamReader(body);
                while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
                {
                    var tweet = reader.ReadLine();
                    var currentTweet = JsonConvert.DeserializeObject<TwitterResponse>(tweet);
                    // make sure the current tweet is not null
                    if (currentTweet != null && currentTweet.Data != null)
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

        /// <summary>
        /// report on the data
        /// </summary>
        /// <param name="e"></param>
        private void OnProcessCompleted(ReportingData e)
        {
            ReportingData?.Invoke(this, e);
        }

        /// <summary>
        /// init the client
        /// </summary>
        /// <param name="authToken"></param>
        private void InitializeHttpClient(string authToken)
        {
            _client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite)
            };
            _client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", authToken);
        }
    }
}
