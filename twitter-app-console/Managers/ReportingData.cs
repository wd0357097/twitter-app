using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using twitter_app_console.Extensions;

namespace twitter_app_console
{
    /// <summary>
    /// used for reporting twitter data stats
    /// </summary>
    public class ReportingData : EventArgs, IReportingData
    {
        private Dictionary<string, int> _emojiesInTweets;
        private Dictionary<string, int> _hashTagsTweets;
        private Dictionary<string, int> _urlTweets;
        private int photoUrlInTweetCounter = 0;
        private TwitterResponse currentTweet;

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
        { get => currentTweet; 
            set 
            { 
                currentTweet = value; 
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
        public double PercentOfTweetsThatContainsEmojis => this._emojiesInTweets.Count().CalculatePercentage(this.TotalNumberOfTweets);
        /// <summary>
        /// returns the percent of tweets that contain photo urls
        /// </summary>
        public double PercentOfTweetsThatContainPhotoUrl => this.PhotoUrlsInTweets().CalculatePercentage(this.TotalNumberOfTweets);
        /// <summary>
        /// returns the percent of tweets that contain urls
        /// </summary>
        public double PercentOfTweetsThatContainUrl => this._urlTweets.Count().CalculatePercentage(this.TotalNumberOfTweets);
        /// <summary>
        /// calculates the photos in tweets based on the current tweet
        /// </summary>
        /// <returns></returns>
        private int PhotoUrlsInTweets()
        {
            var regex = @"[^\\s] + (.*?)\\.(jpg | jpeg | png | gif)$";// this may not be right
            var match = Regex.Match(this.CurrentTweet.Data.Text, regex, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                photoUrlInTweetCounter++;
            }
            return photoUrlInTweetCounter;
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
            var regex = @"#\w+";
            return this._hashTagsTweets.RegexDataToDictionary(regex, this.CurrentTweet.Data.Text);
        }
        /// <summary>
        /// returns a dictionary of urls in tweets
        /// </summary>
        /// <returns>dictionary</returns>
        public Dictionary<string, int> UrlsInTweets()
        {
            var regex = @"(?:(?:https?|ftp|file):\/\/|www\.|ftp\.)(?:\([-A-Z0-9+&@#\/%=~_|$?!:,.]*\)|[-A-Z0-9+&@#\/%=~_|$?!:,.])*(?:\([-A-Z0-9+&@#\/%=~_|$?!:,.]*\)|[A-Z0-9+&@#\/%=~_|$])";
            return this._urlTweets.RegexDataToDictionary(regex, this.CurrentTweet.Data.Text);
        }
        /// <summary>
        /// returns a dictionary of emojis in tweets
        /// </summary>
        /// <returns>dictionary</returns>
        public Dictionary<string, int> EmojisInTweets()
        {
            var regex = @"\u00a9|\u00ae|[\u2000-\u3300] |\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff]";
            return this._emojiesInTweets.RegexDataToDictionary(regex, this.CurrentTweet.Data.Text);
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
