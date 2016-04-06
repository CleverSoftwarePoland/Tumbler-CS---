using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Clever.Win.Tumblr.Helpers
{
    //public class WebBrowserExtension
    //{
    //    public  string HtmlSource
    //    {
    //        get { return GetValue(HtmlSourceProperty) as string; }
    //        set { SetValue(HtmlSourceProperty, value); }

    //    }

    //    // Using a DependencyProperty as the backing store for HTML.  This enables animation, styling, binding, etc...  
    //    public static readonly DependencyProperty HtmlSourceProperty =
    //        DependencyProperty.RegisterAttached("HtmlSource", typeof(string), typeof(WebBrowserExtension), new PropertyMetadata(0, new PropertyChangedCallback(OnHTMLChanged)));

    //    private static void OnHTMLChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        WebBrowser wv = d as WebBrowser;
    //        if (wv != null)
    //        {
    //            wv.NavigateToString((string)e.NewValue);
    //        }
    //    }



    //}

    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserUtility), new PropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                browser.NavigateToString(uri);
            }
        }
    }
}
