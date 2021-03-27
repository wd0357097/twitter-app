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
        double PercentOfTweetsThatContainsEmojis { get; }
        Dictionary<string, int> HashTagsInTweets();
        double PercentOfTweetsThatContainUrl { get; set; }
        double TweetsThatContainPhotoUrl { get; set; }
        string TopDomainsOfUrlsInTweets { get; set; }
        Dictionary<string, int> UrlsInTweets();
        TimeSpan TimeCounter { get; }
    }
}
