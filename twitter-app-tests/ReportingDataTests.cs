using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using twitter_data.Entities;
using twitter_data.Interface;
using twitter_data.Managers;

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
            _data.EmojisInTweets().FirstOrDefault().Key.ShouldBe("🥺");
        }

        [TestMethod]
        public void percent_of_tweets_that_contain_emojis_test()
        {
            _data.PercentOfTweetsThatContainsEmojis.ShouldBe(31.58d);
        }

        [TestMethod]
        public void top_hash_tags_in_tweets_test()
        {
            _data.HashTagsInTweets().FirstOrDefault().Key.ShouldBe("#RealmenteZacatecas");
        }

        [TestMethod]
        public void percent_of_tweets_that_contain_urls_test()
        {
            _data.PercentOfTweetsThatContainUrl.ShouldBe(42.11d);
        }

        [TestMethod]
        public void percent_of_tweets_that_contain_photo_urls_test()
        {
            _data.PercentOfTweetsThatContainPhotoUrl.ShouldBe(1.32d);
        }

        [TestMethod]
        public void top_urls_in_tweets_test()
        {
            _data.UrlsInTweets().FirstOrDefault().Key.ShouldBe("https://test.jpg");
        }
    }
}
