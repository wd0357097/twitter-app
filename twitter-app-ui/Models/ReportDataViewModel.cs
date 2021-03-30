using System.Collections.Generic;
using System.Linq;

namespace twitter_app_ui.Models
{
    public class ReportDataViewModel
    {
        private Dictionary<string, int> emojis;
        private Dictionary<string, int> hashTags;
        private Dictionary<string, int> urls;

        public int TotalNumberOfTweets { get; set; }
        public double AverageNumberOfTweetsPerHour { get; set; }
        public double AverageNumberOfTweetsPerMinute { get; set; }
        public double AverageNumberOfTweetsPerSecond { get; set; }
        public double PercentOfTweetsThatContainEmojis { get; set; }
        public double PercentOfTweetsThatContainUrl { get; set; }
        public double PercentOfTweetsThatContainPhotoUrl { get; set; }
        public Dictionary<string, int> Top10HashTags
        {
            get => hashTags?.Take(10).ToDictionary(x=>x.Key, x=>x.Value);
            set => hashTags = value;
        }
  
        public Dictionary<string, int> Top10EmojisInTweets
        {
            get => emojis?.Take(10).ToDictionary(x => x.Key, x => x.Value);
            set => emojis = value;
        }

        public Dictionary<string, int> Top10UrlTweets
        {
            get => urls?.Take(10).ToDictionary(x => x.Key, x => x.Value);
            set => urls = value;
        }
    }
}
