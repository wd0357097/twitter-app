using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using twitter_app_console;

namespace twitter_app_tests
{
    [TestClass]
    public class ReportingDataTests
    {
        private IReportingData _data;

        [TestInitialize]
        public void Init() 
        {
            _data = new ReportingData();
            using (var r = new StreamReader("./Artifacts/TwitterSampleData.json"))
            {
                var json = r.ReadToEnd();
                var twitterResponse = JsonConvert.DeserializeObject<List<TwitterResponse>>(json);
                foreach (var t in twitterResponse)
                {
                    _data.CurrentTweet = t;
                }
            }
        }

        [TestMethod]
        public void total_number_of_tweets_test()
        {
            _data.TotalNumberOfTweets.ShouldBe(76);
        }

        //[TestMethod]
        //public void average_number_of_tweets_by_time_test()
        //{
        //    foreach (var t in _twitterResponse)
        //    {
        //        _data.CurrentTweet = t;
        //    }
        //    _data.AverageNumberOfTweets(_data.TimeCounter.TotalHours).ShouldBe(3.15);
        //}

        [TestMethod]
        public void top_emojis_in_tweets_test()
        {
            _data.EmojisInTweets().FirstOrDefault().Key.ShouldBe("“ ");
        }

        [TestMethod]
        public void percent_of_tweets_that_contain_emojis_test()
        {
            _data.PercentOfTweetsThatContainsEmojis.ShouldBe(21.05);
        }

        [TestMethod]
        public void top_hash_tags_in_tweets_test()
        {
            _data.HashTagsInTweets().FirstOrDefault().Key.ShouldBe("#春から立教");
        }

        [TestMethod]
        public void percent_of_tweets_that_contain_urls_test()
        {
            _data.PercentOfTweetsThatContainUrl.ShouldBe(39.47d);
        }

        [TestMethod]
        public void percent_of_tweets_that_contain_photo_urls_test()
        {
            _data.PercentOfTweetsThatContainPhotoUrl.ShouldBe(1.32d);
        }

        [TestMethod]
        public void top_urls_in_tweets_test()
        {
            _data.UrlsInTweets().FirstOrDefault().Key.ShouldBe("https://t.co/qPPF8CeGjF");
        }
    }
}
