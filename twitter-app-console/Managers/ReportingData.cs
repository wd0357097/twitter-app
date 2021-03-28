using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace twitter_app_console
{
    public class ReportingData : EventArgs, IReportingData
    {
        private Dictionary<string, int> _emojiesInTweets;
        private Dictionary<string, int> _hashTagsTweets;
        private Dictionary<string, int> _urlTweets;

        public ReportingData()
        {
            _emojiesInTweets = new Dictionary<string, int>();
            _hashTagsTweets = new Dictionary<string, int>();
            _urlTweets = new Dictionary<string, int>();
        }

        public TwitterResponse CurrentTweet { get; set; }

        public int TotalNumberOfTweets { get; set; }
        public double PercentOfTweetsThatContainsEmojis => this.CalculatePercentage(this._emojiesInTweets.Count());

        public Dictionary<string, int> HashTagsInTweets()
        {
            var regex = @"#\w+";
            var match = Regex.Match(this.CurrentTweet.Data.Text, regex, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                if (this._hashTagsTweets.ContainsKey(match.Value))
                {
                    var value = this._hashTagsTweets.First(x => x.Key == match.Value).Value;
                    this._hashTagsTweets[match.Value] = value + 1;
                }
                else
                {
                    this._hashTagsTweets.Add(match.Value, 1);
                }
            }
            if (this._hashTagsTweets.Count > 0)
            {
                this._hashTagsTweets = this._hashTagsTweets.OrderByDescending(kvp => kvp.Value).ToDictionary(x => x.Key, x => x.Value);
            }
            return _hashTagsTweets;
        }

        public int PhotoUrlsInTweets()
        {
            int count = 0;
            var regex = @"[^\\s] + (.*?)\\.(jpg | jpeg | png | gif)$";// this may not be right
            var match = Regex.Match(this.CurrentTweet.Data.Text, regex, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                count++;
            }
            return count;
        }

        public Dictionary<string, int> UrlsInTweets()
        {
            var regex = @"(?:(?:https?|ftp|file):\/\/|www\.|ftp\.)(?:\([-A-Z0-9+&@#\/%=~_|$?!:,.]*\)|[-A-Z0-9+&@#\/%=~_|$?!:,.])*(?:\([-A-Z0-9+&@#\/%=~_|$?!:,.]*\)|[A-Z0-9+&@#\/%=~_|$])";
            var match = Regex.Match(this.CurrentTweet.Data.Text, regex, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                if (this._urlTweets.ContainsKey(match.Value))
                {
                    var value = this._urlTweets.First(x => x.Key == match.Value).Value;
                    this._urlTweets[match.Value] = value + 1;
                }
                else
                {
                    this._urlTweets.Add(match.Value, 1);
                }
            }
            if (this._urlTweets.Count > 0)
            {
                this._urlTweets = this._urlTweets.OrderByDescending(kvp => kvp.Value).ToDictionary(x => x.Key, x => x.Value);
            }
            return _urlTweets;
        }
        public double PercentOfTweetsThatContainPhotoUrl => this.CalculatePercentage(this.PhotoUrlsInTweets());
        public string TopDomainsOfUrlsInTweets { get; set; }
        public TimeSpan DateStartTime { get; set; }

        public TimeSpan TimeCounter => DateTime.Now.TimeOfDay - this.DateStartTime;

        public double PercentOfTweetsThatContainUrl => this.CalculatePercentage(this._urlTweets.Count());

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
                if (this._emojiesInTweets.ContainsKey(match.Value))
                {
                    var value = this._emojiesInTweets.First(x => x.Key == match.Value).Value;
                    this._emojiesInTweets[match.Value] = value + 1;
                }
                else
                {
                    this._emojiesInTweets.Add(match.Value, 1);
                }
            }
            if (this._emojiesInTweets.Count > 0)
            {
                this._emojiesInTweets = this._emojiesInTweets.OrderByDescending(kvp => kvp.Value).ToDictionary(x => x.Key, x => x.Value);
            }
            return _emojiesInTweets;
        }

        private double CalculatePercentage(int dictionaryCount) 
        {
            return Math.Round(Convert.ToDouble(dictionaryCount) / Convert.ToDouble(this.TotalNumberOfTweets) * 100, 2);
        }

        public override string ToString()
        {
            var emoji = EmojisInTweets().FirstOrDefault();
            var hash = HashTagsInTweets().FirstOrDefault();
            var url = UrlsInTweets().FirstOrDefault();
            return
                $"Total Number Of Tweets: {this.TotalNumberOfTweets}  \r\n" +
                $"Projected Average Number Of Tweets Per Hour: {this.AverageNumberOfTweets(TimeCounter.TotalHours)} \r\n" +
                $"Projected Average Number Of Tweets Per Minute: {this.AverageNumberOfTweets(TimeCounter.TotalMinutes)} \r\n" +
                $"Average Number Of Tweets Per Second: {this.AverageNumberOfTweets(TimeCounter.TotalSeconds)} \r\n" +
                $"Top Emoji in Tweets: {emoji.Key} : Appeared: {emoji.Value} time(s) \r\n" +
                $"Percent of Tweets that Contain Emojis: {this.PercentOfTweetsThatContainsEmojis} \r\n" +
                $"Top HashTag: {hash.Key} : Appeared: {hash.Value} time(s) \r\n" +
                $"Percent of Tweets that contain a url: {this.PercentOfTweetsThatContainUrl} \r\n" +
                $"Percent of Tweets that contain a photo url: {this.PercentOfTweetsThatContainPhotoUrl} \r\n" +
                $"Top url in Tweets: {url.Key} : Appeared: {url.Value} time(s) \r\n";
        }
    }
}
