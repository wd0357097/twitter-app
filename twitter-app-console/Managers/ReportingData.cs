using System;
using System.Collections.Generic;
using System.Text;

namespace twitter_app_console
{
    public class ReportingData : EventArgs, IReportingData
    {
        public List<TwitterResponse> TwitterResponses { get; set; }
        public int TotalNumberOfTweets { get; set; }
        public decimal PercentOfTweetsThatContainsEmojis { get; set; }
        public string TopHashTags { get; set; }
        public decimal TweetsThatContainUrl { get; set; }
        public decimal TweetsThatContainPhotoUrl { get; set; }
        public string TopDomainsOfUrlsInTweets { get; set; }
        public TimeSpan DateStartTime { get; set; }

        public TimeSpan TimeCounter => DateTime.Now.TimeOfDay - DateStartTime;

        public double AverageNumberOfTweets(double time)
        {
            return Math.Round(TotalNumberOfTweets / time, 2);
        }

        public string TopEmojisInTweets(int number)
        {
            return "1";
        }

        public override string ToString()
        {
            return
                $"Total Number Of Tweets: {this.TotalNumberOfTweets}  \r\n" +
                $"Projected Average Number Of Tweets Per Hour: {this.AverageNumberOfTweets(TimeCounter.TotalHours)} \r\n" +
                $"Projected Average Number Of Tweets Per Minute: {this.AverageNumberOfTweets(TimeCounter.TotalMinutes)} \r\n" +
                $"Average Number Of Tweets Per Second: {this.AverageNumberOfTweets(TimeCounter.TotalSeconds)} \r\n" +
                $"#1 Emojis in Tweets: {this.TopEmojisInTweets(1)} \r\n" +
                $"Percent of Tweets that Contain Emojis: {this.PercentOfTweetsThatContainsEmojis} \r\n" +
                $"#1 HashTag: {this.TopHashTags} \r\n" +
                $"Percent of Tweets that contain a url: {this.TweetsThatContainUrl} \r\n" +
                $"Percent of Tweets taht contain a photo url: {this.TweetsThatContainPhotoUrl} \r\n" +
                $"#1 url in Tweets: {this.TopDomainsOfUrlsInTweets} \r\n";
        }
    }
}
