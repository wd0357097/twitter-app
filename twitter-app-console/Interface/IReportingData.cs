using System;
using System.Collections.Generic;
using System.Text;

namespace twitter_app_console
{
    public interface IReportingData
    {
        TwitterResponse CurrentTweet { get; set; }
        TimeSpan DateStartTime { get; set; }
        int TotalNumberOfTweets { get; set; }
        double AverageNumberOfTweets(double time);
        Dictionary<string, int> EmojisInTweets();
        decimal PercentOfTweetsThatContainsEmojis { get; set; }
        string TopHashTags { get; set; }
        decimal TweetsThatContainUrl { get; set; }
        decimal TweetsThatContainPhotoUrl { get; set; }
        string TopDomainsOfUrlsInTweets { get; set; }
        TimeSpan TimeCounter { get; }
    }
}
