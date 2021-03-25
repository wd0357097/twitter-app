using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

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
        public async Task StartStreamAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await _client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead);
            using (var body = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(body))
                while (!reader.EndOfStream)
                    Console.WriteLine(reader.ReadLine());
        }

        private void InitializeHttpClient()
        {
            _client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite)
            };
            // TODO, Store this somewhere not in the code
            _client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["token"]);
        }
    }
}
