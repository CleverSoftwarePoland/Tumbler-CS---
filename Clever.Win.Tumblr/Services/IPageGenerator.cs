using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.Services
{
    public interface IPageGenerator
    {
        string CreateWebPagPhotoDetails(float font, string title, string coverPhoto);
        string CreateWebPagPhotoInfo(float font, string title, string coverPhoto);
        string CreateWebPagRegularDetails(float font, string title, string item);
        string CreateWebPagRegularInfo(float font, string title, string item);
        string CreateWebPagVideoDetails(float font, string title, string videoItem);
        string CreateWebPagVideoInfo(float font, string title, string videoItem);
    }
}
