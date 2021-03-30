using Microsoft.VisualStudio.TestTools.UnitTesting;
using twitter_data.Entities;
using twitter_data.Interface;
using twitter_data.Managers;
using Shouldly;
using Moq;
using twitter_app_tests.Mocks;
using System.Net.Http;
using System;
using System.Threading;

namespace twitter_app_tests
{
    [TestClass]
    public class TwitterStreamTests
    {
        private IAppStream _data;
        private Mock<MockHttpHandler> _mockHttpClient;

        [TestInitialize]
        public void init()
        {
            _mockHttpClient = new Mock<MockHttpHandler> { CallBase = true };
            _data = new TwitterStream(new HttpClient(_mockHttpClient.Object));
        }

        [TestMethod]
        public async System.Threading.Tasks.Task start_stream_async_testAsync()
        {
            _mockHttpClient.Setup(p => p.GetAsync(It.IsAny<Uri>()));
            await _data.StartStreamAsync("https://api.twitter.com/2/tweets/sample/stream", new CancellationToken());
        }
    }
}
