using Clever.Win.Tumblr.Services;
using Clever.Win.Tumblr.Services.Data;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.ViewModel
{
    public class PostInfoViewModel : ViewModelBase
    {
        IPageGenerator _pageGenerator = ServiceLocator.Current.GetInstance<IPageGenerator>();

        private PostDataBase _postData;
        public PostDataBase PostData
        {
            get
            {
                return _postData;
            }
        }


        public PostInfoViewModel(PostDataBase source)
        {
            _postData = source;
        }

        public string HtmlSource
        {
            get
            {
                if (PostData.Type == "photo")
                {
                    var photo = PostData as PhotoPostData;
                    return _pageGenerator.CreateWebPagPhotoInfo(0.4f, photo.PhoyoCaption, photo.PhoyoUrl400);
                }

                if (PostData.Type == "video")
                {
                    var video = PostData as VideoPostData;
                    return _pageGenerator.CreateWebPagVideoInfo(0.4f, video.VideoCaption, video.VideoPlayer);
                }

                if (PostData.Type == "regular")
                {
                    var regular = PostData as RegularPostData;
                    return _pageGenerator.CreateWebPagRegularInfo(1.2f, regular.RegularTitle, regular.RegularBody);
                }

                return string.Empty;
            }
        }

       
    }
}
