using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.Services.Data
{
    public class VideoPostData : PostDataBase
    {
        [JsonProperty("video-source")]
        public string VideoSource { get; set; }

        [JsonProperty("video-player")]
        public string VideoPlayer { get; set; }

        [JsonProperty("video-caption")]
        public string VideoCaption { get; set; }
    }
}
