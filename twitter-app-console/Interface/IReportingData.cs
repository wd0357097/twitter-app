using System;
using System.Collections.Generic;

namespace twitter_app_console
{
    public interface IReportingData
    {
        TwitterResponse CurrentTweet { get; set; }
        TimeSpan DateStartTime { get; set; }
        int TotalNumberOfTweets { get; set; }
        double AverageNumberOfTweets(double time);
        Dictionary<string, int> EmojisInTweets();
        double PercentOfTweetsThatContainsEmojis { get; }
        Dictionary<string, int> HashTagsInTweets();
        double PercentOfTweetsThatContainUrl { get; }
        double PercentOfTweetsThatContainPhotoUrl { get; }
        Dictionary<string, int> UrlsInTweets();
        TimeSpan TimeCounter { get; }
    }
}
