using System;
using System.Collections.Generic;
using System.Text;

namespace twitter_app_console
{
    public interface IReportingData
    {
        int TotalNumberOfTweets { get; }
        int AverageNumberOfTweets(TimeSpan time);
        string TopEmojisInTweets(int number);
        decimal PercentOfTweetsThatContainsEmojis { get; }
        string TopHashTags { get; }
        decimal TweetsThatContainUrl { get; }
        decimal TweetsThatContainPhotoUrl { get; }
        string TopDomainsOfUrlsInTweets { get; }
    }
}
