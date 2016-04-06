using Clever.Win.Tumblr.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.Services
{
    public interface ITumblrServiceProxy
    {
        Task<FeedData> GetProfileData(string user = "qbn-scholar");
    }
}
