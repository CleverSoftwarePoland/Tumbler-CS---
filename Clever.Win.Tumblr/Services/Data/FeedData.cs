using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.Services.Data
{
    public class FeedData
    {
        public UserData UserData { get; set; }

        public int PostsTotal { get; set; }

        [JsonProperty("Posts")]
        internal List<JObject> PostsInternal { get; set; }

        [JsonProperty("OtherPosts")]
        public List<PostDataBase> Posts
        {
            get
            {
                var convertedPosts = new List<PostDataBase>();

                foreach (JObject item in PostsInternal)
                {
                    if (item.Root["type"].Value<string>() == "photo")
                    {
                        convertedPosts.Add(item.ToObject<PhotoPostData>());
                    }

                    if (item.Root["type"].Value<string>() == "video")
                    {
                        convertedPosts.Add(item.ToObject<VideoPostData>());
                    }


                    if (item.Root["type"].Value<string>() == "regular")
                    {
                        convertedPosts.Add(item.ToObject<RegularPostData>());
                    }
                }
                return convertedPosts;
            }
        }


        //public List<PostDataBase> PostList { get; set; }
    }
}
