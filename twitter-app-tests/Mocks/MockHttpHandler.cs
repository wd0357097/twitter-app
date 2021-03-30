using System;
using System.IO;
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
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            return await Task.FromResult(response);
        }

        public abstract HttpResponseMessage GetAsync(Uri url);
    }
}
