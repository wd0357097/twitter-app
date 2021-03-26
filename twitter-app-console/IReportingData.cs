using System;
using System.Collections.Generic;
using System.Text;

namespace twitter_app_console
{
    public interface IReportingData
    {
        int TotalNumberOfTweets { get; set; }
        double AverageNumberOfTweets(double time);
        string TopEmojisInTweets(int number);
        decimal PercentOfTweetsThatContainsEmojis { get; set; }
        string TopHashTags { get; set; }
        decimal TweetsThatContainUrl { get; set; }
        decimal TweetsThatContainPhotoUrl { get; set; }
        string TopDomainsOfUrlsInTweets { get; set; }
    }
}
