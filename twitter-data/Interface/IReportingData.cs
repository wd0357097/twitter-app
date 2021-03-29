using System;
using System.Collections.Generic;
using twitter_data.Entities;

namespace twitter_data.Interface
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
