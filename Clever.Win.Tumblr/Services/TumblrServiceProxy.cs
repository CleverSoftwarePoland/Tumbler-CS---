using Clever.Win.Tumblr.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.Services
{
    public class TumblrServiceProxy : ITumblrServiceProxy
    {
        public async Task<FeedData> GetProfileData(string user = "qbn-scholar")
        {
            return await WebRequestWrapper.GetAsync<FeedData>(string.Format("http://www.{0}.tumblr.com/api/read/json", user));
        }
    }
}
