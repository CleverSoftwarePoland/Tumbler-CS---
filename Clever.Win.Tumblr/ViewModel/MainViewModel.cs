using Clever.Win.Tumblr.Services;
using Clever.Win.Tumblr.Services.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Clever.Win.Tumblr.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ITumblrServiceProxy _proxyData = ServiceLocator.Current.GetInstance<ITumblrServiceProxy>();

        private Visibility _isLoading = Visibility.Collapsed;

        public Visibility IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        private ObservableCollection<PostInfoViewModel> _postCollection = new ObservableCollection<PostInfoViewModel>();

        public ObservableCollection<PostInfoViewModel> PostCollection
        {
            get
            {
                return _postCollection;
            }

            set
            {
                _postCollection = value;
                RaisePropertyChanged(() => PostCollection);
            }
        }

        private PostInfoViewModel _selectedItem;

        public PostInfoViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if (value == null) return;
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
                App.PostData = value.PostData;
                App.RootFrame.Navigate(new Uri("/Views/PostPage.xaml", UriKind.RelativeOrAbsolute));


                _selectedItem = null;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public string UserProfileName
        {
            get
            {
                return _userProfileName;
            }

            set
            {
                _userProfileName = value;
                RaisePropertyChanged(() => UserProfileName);
            }
        }

        private string _userProfileName = string.Empty;

        public ICommand SearchCommand { get; private set; }

      



        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            AttachCommands();
        }

        private void AttachCommands()
        {
            SearchCommand = new RelayCommand(OnSearchCmdCallback);
        }

        private async void OnSearchCmdCallback()
        {
            if(string.IsNullOrEmpty(UserProfileName))
            {
                MessageBox.Show("WprowadŸ nazwê u¿ytkownika!");
                return;
            }

            await Load(UserProfileName);
        }

        public async Task Load(string userProfileName)
        {
            FeedData feedData = null;

            if (IsLoading == Visibility.Visible) return;

            _postCollection.Clear();

            try
            {
                IsLoading = Visibility.Visible;
                feedData = await _proxyData.GetProfileData(userProfileName);
            }
            catch (Exception ex)
            {
                IsLoading = Visibility.Collapsed;
                MessageBox.Show("SprawdŸ po³¹czenie danych i spróbuj ponownie!");
                return;
            }

            if (feedData.Posts == null) return;

            foreach (var item in feedData.Posts)
            {
                if (item.Type == "video") continue;
                _postCollection.Add(new PostInfoViewModel(item));
            }

            IsLoading = Visibility.Collapsed;
        }
    }
}