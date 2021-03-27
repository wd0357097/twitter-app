using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace twitter_app_console
{
    public class ReportingData : EventArgs, IReportingData
    {
        private Dictionary<string, int> _emojiesInTweets;

        public ReportingData() 
        {
            _emojiesInTweets = new Dictionary<string, int>();
        }

        public List<TwitterResponse> TwitterResponses { get; set; }
        public int TotalNumberOfTweets { get; set; }
        public decimal PercentOfTweetsThatContainsEmojis { get; set; }
        public string TopHashTags { get; set; }
        public decimal TweetsThatContainUrl { get; set; }
        public decimal TweetsThatContainPhotoUrl { get; set; }
        public string TopDomainsOfUrlsInTweets { get; set; }
        public TimeSpan DateStartTime { get; set; }

        public TimeSpan TimeCounter => DateTime.Now.TimeOfDay - DateStartTime;

        public TwitterResponse CurrentTweet { get; set; }

        public double AverageNumberOfTweets(double time)
        {
            return Math.Round(TotalNumberOfTweets / time, 2);
        }

        public Dictionary<string, int> EmojisInTweets()
        {
            var regex = @"\u00a9|\u00ae|[\u2000-\u3300] |\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff]";
            var match = Regex.Match(this.CurrentTweet.Data.Text, regex, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                if (_emojiesInTweets.ContainsKey(match.Value))
                {
                    var value = _emojiesInTweets.First(x => x.Key == match.Value).Value;
                    _emojiesInTweets[match.Value] = value + 1;
                }
                else
                {
                    _emojiesInTweets.Add(match.Value, 1);
                }
            }
            if(_emojiesInTweets.Count > 0)
            {
                _emojiesInTweets.OrderBy(kvp => kvp.Value).ToDictionary(x => x.Key, x => x.Value);
            }
            return _emojiesInTweets;
        }

        public override string ToString()
        {
            return
                $"Total Number Of Tweets: {this.TotalNumberOfTweets}  \r\n" +
                $"Projected Average Number Of Tweets Per Hour: {this.AverageNumberOfTweets(TimeCounter.TotalHours)} \r\n" +
                $"Projected Average Number Of Tweets Per Minute: {this.AverageNumberOfTweets(TimeCounter.TotalMinutes)} \r\n" +
                $"Average Number Of Tweets Per Second: {this.AverageNumberOfTweets(TimeCounter.TotalSeconds)} \r\n" +
                $"#1 Emojis in Tweets: {this.EmojisInTweets().FirstOrDefault()} \r\n" +
                $"Percent of Tweets that Contain Emojis: {this.PercentOfTweetsThatContainsEmojis} \r\n" +
                $"#1 HashTag: {this.TopHashTags} \r\n" +
                $"Percent of Tweets that contain a url: {this.TweetsThatContainUrl} \r\n" +
                $"Percent of Tweets taht contain a photo url: {this.TweetsThatContainPhotoUrl} \r\n" +
                $"#1 url in Tweets: {this.TopDomainsOfUrlsInTweets} \r\n";
        }
    }
}
