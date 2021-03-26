using System;
using System.Collections.Generic;
using System.Text;

namespace twitter_app_console
{
    public class ReportingData : EventArgs, IReportingData
    {
        public int TotalNumberOfTweets { get; set; }
        public decimal PercentOfTweetsThatContainsEmojis { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TopHashTags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal TweetsThatContainUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal TweetsThatContainPhotoUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string TopDomainsOfUrlsInTweets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double AverageNumberOfTweets(double time)
        {
            return time / TotalNumberOfTweets;
        }

        public string TopEmojisInTweets(int number)
        {
            throw new NotImplementedException();
        }
    }
}
