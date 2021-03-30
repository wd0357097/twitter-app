using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace twitter_app_tests.Mocks
{
    /// <summary>
    /// https://dev.to/sharathns/mocking-httpclients-using-httpclienthandler-in-c-iee
    /// </summary>
    public abstract class MockHttpHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"  {""data"": {""id"": ""1375595996915318786"", ""text"": ""@biafz91 .""}}"),
            };
            return await Task.FromResult(response);
        }

        public abstract HttpResponseMessage GetAsync(Uri url);
    }
}
