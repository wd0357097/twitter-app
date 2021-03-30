
using Newtonsoft.Json;
using System;

namespace twitter_data.Entities
{ 
    public class TwitterResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("includes")]
        public Includes Includes { get; set; }
    }

    public class Data
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("attachments")]
        public Attachments Attachments { get; set; }
    }

    public partial class Attachments
    {
        [JsonProperty("media_keys")]
        public string[] MediaKeys { get; set; }
    }

    public partial class Includes
    {
        [JsonProperty("media")]
        public Media[] Media { get; set; }
    }

    public partial class Media
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("media_key")]
        public string MediaKey { get; set; }

        [JsonProperty("preview_image_url")]
        public Uri PreviewImageUrl { get; set; }
    }

    public partial class Entities
    {
        [JsonProperty("hashtags")]
        public HashTag[] Hashtags { get; set; }

        [JsonProperty("urls")]
        public Url[] Urls { get; set; }
    }

    public partial class Annotation
    {
        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("probability")]
        public double Probability { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("normalized_text")]
        public string NormalizedText { get; set; }
    }

    public partial class HashTag
    {
        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }
    }

    public partial class Url
    {
        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("url")]
        public Uri UrlUrl { get; set; }

        [JsonProperty("expanded_url")]
        public Uri ExpandedUrl { get; set; }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }
    }
}



