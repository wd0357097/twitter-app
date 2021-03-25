using System;
using System.Collections.Generic;
using System.Text;

namespace twitter_app_console
{
    public interface IReportingData
    {

        int TotalNumberOfTweets();
        int AverageNumberOfTweets(TimeSpan time);
        string TopEmojisInTweets(int number);
        decimal PercentOfTweetsThatContainsEmojis();
        string TopHashTags();
        decimal TweetsThatContainUrl();
        decimal TweetsThatContainPhotoUrl();
        string TopDomainsOfUrlsInTweets();
    }
}
