using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.Services.Data
{
    public class PostDataBase
    {
        public string Id { get; set; }

        public string Url { get; set; }

        [JsonProperty("url-with-slug")]
        public string UrlWithSlug { get; set; }

        public string Type { get; set; }

        public string Format { get; set; }

        [JsonProperty("unix-timestamp")]
        public int UnixTimeStamp { get; set; }

        public string Slug { get; set; }


    }
}
