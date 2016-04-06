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
    public class PostPageViewModel :ViewModelBase
    {
        IPageGenerator _pageGenerator = ServiceLocator.Current.GetInstance<IPageGenerator>();

        private PostDataBase _postData;

        public async Task Load()
        {
            _postData = App.PostData;
            RaisePropertyChanged(null);
        }

        public string HtmlSource
        {
            get
            {
                if (_postData == null) return string.Empty;

                if (_postData.Type == "photo")
                {
                    var photo = _postData as PhotoPostData;
                    return _pageGenerator.CreateWebPagPhotoDetails(0.4f, photo.PhoyoCaption, photo.PhoyoUrl400);
                }

                if (_postData.Type == "video")
                {
                    var video = _postData as VideoPostData;
                    return _pageGenerator.CreateWebPagVideoDetails(0.4f, video.VideoCaption, video.VideoPlayer);
                }

                if (_postData.Type == "regular")
                {
                    var regular = _postData as RegularPostData;
                    return _pageGenerator.CreateWebPagRegularDetails(1.2f, regular.RegularTitle, regular.RegularBody);
                }

                return string.Empty;
            }
        }
    }
}
