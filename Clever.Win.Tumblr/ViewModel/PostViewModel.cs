using Clever.Win.Tumblr.Services;
using Clever.Win.Tumblr.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.ViewModel
{
    public class PostViewModel
    {
        PageGenerator _pageGenerator = new PageGenerator();

        private PostDataBase _postData;

        public PostViewModel(PostDataBase source)
        {
            _postData = source;
        }

        public string HtmlSource
        {
            get
            {
                if (_postData.Type == "photo")
                {
                    var photo = _postData as PhotoPostData;
                    return _pageGenerator.CreateWebPagPhotoInfo(0.4f, photo.PhoyoCaption, photo.PhoyoUrl400);
                }

                if (_postData.Type == "video")
                {
                    var video = _postData as VideoPostData;
                    return _pageGenerator.CreateWebPagVideoInfo(0.4f, video.VideoCaption, video.VideoPlayer);
                }

                if (_postData.Type == "regular")
                {
                    var regular = _postData as RegularPostData;
                    return _pageGenerator.CreateWebPagRegularInfo(1.2f, regular.RegularTitle, regular.RegularBody);
                }

                return string.Empty;
            }
        }
    }
}
