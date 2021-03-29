using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System.IO;

namespace twitter_app_tests
{
    [TestClass]
    public class ReportingDataTests
    {
        private string _twitterJson;
        //private List<Twitter>

        [TestInitialize]
        public void Init() 
        {
            using (var r = new StreamReader("./Artifacts/TwitterSampleData.json"))
            {
                this._twitterJson = r.ReadToEnd();
            }

            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When("http://localhost/api/user/*")
                    .Respond("application/json", "{'name' : 'Test McGee'}"); // Respond with JSON

            // Inject the handler or client into your application code
            var client = mockHttp.ToHttpClient();

            //var response = await client.GetAsync("http://localhost/api/user/1234");
            //// or without async: var response = client.GetAsync("http://localhost/api/user/1234").Result;

            //var json = await response.Content.ReadAsStringAsync();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }

    }
}
