using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Clever.Win.Tumblr.ViewModel;

namespace Clever.Win.Tumblr.Views
{
    public partial class PostPage : PhoneApplicationPage
    {
        private PostPageViewModel _viewModel;

        public PostPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                DataContext = _viewModel = new PostPageViewModel();
                await _viewModel.Load();
            }
        }
    }
}