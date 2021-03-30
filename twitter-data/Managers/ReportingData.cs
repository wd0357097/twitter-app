using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using twitter_data.Entities;
using twitter_data.Extensions;
using twitter_data.Interface;

namespace twitter_data.Managers
{
    /// <summary>
    /// used for reporting twitter data stats
    /// </summary>
    public class ReportingData : EventArgs, IReportingData
    {
        private Dictionary<string, int> _emojiesInTweets;
        private Dictionary<string, int> _hashTagsTweets;
        private Dictionary<string, int> _urlTweets;
        private int _photoUrlInTweetCounter = 0;
        private int _urlInTweetCounter = 0;
        private int _emojisInTweetCounter = 0;
        private TwitterResponse _currentTweet;
        private readonly string _photoMediaType = "photo";

        public ReportingData()
        {
            _emojiesInTweets = new Dictionary<string, int>();
            _hashTagsTweets = new Dictionary<string, int>();
            _urlTweets = new Dictionary<string, int>();
        }

        /// <summary>
        /// stores the current tweet in an object
        /// </summary>
        public TwitterResponse CurrentTweet 
        { get => _currentTweet; 
            set 
            { 
                _currentTweet = value; 
                this.TotalNumberOfTweets++;
                this.EmojisInTweets();
                this.HashTagsInTweets();
                this.UrlsInTweets();
            }
        }
        /// <summary>
        /// stores the total number of tweets processed 
        /// </summary>
        public int TotalNumberOfTweets { get; set; }
        /// <summary>
        /// Start time of processing 
        /// </summary>
        public TimeSpan DateStartTime { get; set; }
        /// <summary>
        /// Time Counter used to determine the number of seconds between when data started to process
        /// </summary>
        public TimeSpan TimeCounter => DateTime.Now.TimeOfDay - this.DateStartTime;
        /// <summary>
        /// returns the percent of tweets that contain emojis
        /// </summary>
        public double PercentOfTweetsThatContainsEmojis => this._emojisInTweetCounter.CalculatePercentage(this.TotalNumberOfTweets);
        /// <summary>
        /// returns the percent of tweets that contain photo urls
        /// </summary>
        public double PercentOfTweetsThatContainPhotoUrl => this.PhotoUrlsInTweets().CalculatePercentage(this.TotalNumberOfTweets);
        /// <summary>
        /// returns the percent of tweets that contain urls
        /// </summary>
        public double PercentOfTweetsThatContainUrl => this._urlInTweetCounter.CalculatePercentage(this.TotalNumberOfTweets);
        /// <summary>
        /// calculates the photos in tweets based on the current tweet
        /// </summary>
        /// <returns></returns>
        private int PhotoUrlsInTweets()
        {
            var media = this.CurrentTweet.Includes?.Media;
            if (media != null && media.Any())
            {
                foreach (var m in media)
                {
                    if (m.Type == this._photoMediaType)
                    {
                        _photoUrlInTweetCounter++;
                    }
                }
            }
            return _photoUrlInTweetCounter;
        }
        /// <summary>
        /// calculates the averages of tweets 
        /// </summary>
        /// <param name="time"></param>
        /// <returns>average number (double)</returns>
        public double AverageNumberOfTweets(double time)
        {
            return Math.Round(TotalNumberOfTweets / time, 2);
        }
        /// <summary>
        /// returns a dictionary of hash tags in tweets
        /// </summary>
        /// <returns>dictionary</returns>
        public Dictionary<string, int> HashTagsInTweets()
        {
            var hashTags = this.CurrentTweet.Data.Entities?.Hashtags;
            if (hashTags != null)
            {
                foreach(var h in hashTags)
                {
                    if (_hashTagsTweets.ContainsKey(h.Tag))
                    {
                        var value = _hashTagsTweets.First(x => x.Key == h.Tag).Value;
                        _hashTagsTweets[h.Tag] = value + 1;
                    }
                    else
                    {
                        _hashTagsTweets.Add(h.Tag, 1);
                    }
                }
            }
            return _hashTagsTweets.OrderDictionary();
        }
        /// <summary>
        /// returns a dictionary of urls in tweets
        /// </summary>
        /// <returns>dictionary</returns>
        public Dictionary<string, int> UrlsInTweets()
        {
            var urls = this.CurrentTweet.Data.Entities?.Urls;
            if (urls != null)
            {
                foreach (var u in urls)
                {
                    if (_urlTweets.ContainsKey(u.ExpandedUrl.IdnHost))
                    {
                        var value = _urlTweets.First(x => x.Key == u.ExpandedUrl.IdnHost).Value;
                        _urlTweets[u.ExpandedUrl.IdnHost] = value + 1;
                    }
                    else
                    {
                        _urlTweets.Add(u.ExpandedUrl.IdnHost, 1);
                    }
                }
                _urlInTweetCounter++;// add 1 to the counter
            }
            return _urlTweets.OrderDictionary();
        }
        /// <summary>
        /// returns a dictionary of emojis in tweets
        /// </summary>
        /// <returns>dictionary</returns>
        public Dictionary<string, int> EmojisInTweets()
        {
            var regex = @"\u00a9|\u00ae|[\u2000-\u3300] |\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff]";
            var dicRegex = new Regex(regex, RegexOptions.IgnoreCase);
            var allMatches = dicRegex.Matches(this.CurrentTweet.Data.Text);
            if (allMatches.Count() > 0)
            {
                _emojisInTweetCounter++;
            }
            foreach (Match match in allMatches)
            {
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
            }
            return _emojiesInTweets.OrderDictionary();
        }
        /// <summary>
        /// override the 2 screen for display purposes 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var emoji = _emojiesInTweets.FirstOrDefault();
            var hash = _hashTagsTweets.FirstOrDefault();
            var url = _urlTweets.FirstOrDefault();
            return
                $"Total Number Of Tweets: {this.TotalNumberOfTweets}  \r\n" +
                $"Projected Average Number Of Tweets Per Hour: {this.AverageNumberOfTweets(this.TimeCounter.TotalHours)} \r\n" +
                $"Projected Average Number Of Tweets Per Minute: {this.AverageNumberOfTweets(this.TimeCounter.TotalMinutes)} \r\n" +
                $"Average Number Of Tweets Per Second: {this.AverageNumberOfTweets(this.TimeCounter.TotalSeconds)} \r\n" +
                $"Top Emoji in Tweets: {emoji.Key} : Appeared: {emoji.Value} time(s) \r\n" +
                $"Percent of Tweets that Contain Emojis: {this.PercentOfTweetsThatContainsEmojis} \r\n" +
                $"Top HashTag: {hash.Key} : Appeared: {hash.Value} time(s) \r\n" +
                $"Percent of Tweets that contain a url: {this.PercentOfTweetsThatContainUrl} \r\n" +
                $"Percent of Tweets that contain a photo url: {this.PercentOfTweetsThatContainPhotoUrl} \r\n" +
                $"Top urls in Tweets: {url.Key} : Appeared: {url.Value} time(s) \r\n";
        }
    }
}
