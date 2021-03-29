using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using twitter_app_console;

namespace twitter_app_tests
{
    [TestClass]
    public class ReportingDataTests
    {
        private string _twitterJson;
        private IReportingData _data;
        private List<TwitterResponse> _twitterResponse;
        //private List<Twitter>

        [TestInitialize]
        public void Init() 
        {
            _data = new ReportingData();
            using (var r = new StreamReader("./Artifacts/TwitterSampleData.json"))
            {
                var json = r.ReadToEnd();
                this._twitterResponse = JsonConvert.DeserializeObject<List<TwitterResponse>>(json);
            }
        }

        [TestMethod]
        public void TestTotalNumberOfTweets()
        {
            var count = 0; 
            foreach (var t in _twitterResponse)
            {
                _data.CurrentTweet = t;
                count++;
            }
            _data.TotalNumberOfTweets.ShouldBe(5);
        }

    }
}
