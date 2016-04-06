using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.Services.Data
{
    public class RegularPostData : PostDataBase
    {
        [JsonProperty("regular-title")]
        public string RegularTitle { get; set; }

        [JsonProperty("regular-body")]
        public string RegularBody { get; set; }
    }
}
