using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.Services
{
    public class PageGenerator : IPageGenerator
    {
        private static string CSS_SOURCE = "<style type=\"text/css\"> " +
            "body { background-color:#FFFFFF; font-size: #FONT_SIZE#; margin:0px; color: #BBBBBB !important; font-family: #FONT_FAMILY#; width:100%} " +
            "a {color :#555555; font-size: 1.0em; text-decoration:none; font-family: #FONT_FAMILY#;}" +
            "b {font-size: 1.0em; font-family: #FONT_FAMILY#;}" +
            "img {display: block; max-width:100%; border: 0px solid #636363; text-align: center; margin:10px auto; height: auto;} " +
            "iframe {display: block; margin-left: auto; margin-right: auto; width:100% !important; height:auto !important;  scrolling:no !important;} " +
            ".feedSourceTitle {font-size: 1.5em; font-weight: 600; color: #00BCD4 !important; line-height: 1.5em; font-family: #FONT_FAMILY#;  } " +
            ".imageHolder {  width: 720px;  margin-left: auto;  margin-right: auto;}" +
            ".imageHolder img{ width:100%; margin:0px;}" +
            ".imageHolder .caption { display:none; position: absolute; width:100%; bottom: 0px; left: 0px; color: #ffffff; background: #000000; text-align:left; font-weight:bold; opacity:0.7; padding-left: 5px; padding-right: 5px; padding-top:5px; padding-bottom:5px;}" +
            "h1 {font-size: 1.4em; font-weight: 700; color: #000000 !important; line-height: 1.5em; font-family: #FONT_FAMILY#;} " +
            "h2 {font-size: 1.3em; font-weight: 600; color: #000000 !important; line-height: 1.3em; font-family: #FONT_FAMILY#;} " +
            "h3 {font-size: 1.2em; font-weight: 600; color: #000000 !important; line-height: 1.2em; font-family: #FONT_FAMILY#;} " +
            "#authorLine {color: #666666; font-size: 1.0em; line-height:  1.5em; margin-bottom:10px; font-weight:400; font-family: #FONT_FAMILY#;} " +
            "blockquote { font-size: 1.0em; line-height:  1.3em; margin-top: 10px; margin-bottom: 10px; font-family: #FONT_FAMILY#; color:#555555 !important; font-weight:800;} " +
            "blockquote p{ font-size: 1.0em; line-height:  1.3em; margin-top: 10px; margin-bottom: 10px; font-family: #FONT_FAMILY#; color:#555555 !important; font-weight:800;} " +
            "p { font-size: 1.0em; line-height: 1.5em; margin-top: 20px; margin-bottom: 20px; font-family: #FONT_FAMILY#; color:#555555 !important; } " +
            "span { font-size: 1.0em; line-height: 1.3em; margin-top: 20px; margin-bottom: 20px; font-family: #FONT_FAMILY#; color:#555555 !important; } " +
            "div { max-width:100% !important; font-size: 1.0em; line-height: 1.3em; font-family: #FONT_FAMILY#;} " +
            "#content  {color:#555555 !important; margin: 12px; font-size: 1em; line-height: 1.3em; word-wrap: break-word; font-family: #FONT_FAMILY#; text-align:left; text-justify:inter-word;   margin-left: auto;  margin-right: auto; } " +
            "@media all and (max-width: 799px) and (min-width: 320px) { #content { width: 90%; }}" +
            "@media all and (max-width: 2599px) and (min-width: 800px) { #content { width: 720px; }}" +
            "@media all and (max-width: 799px) and (min-width: 320px) { iframe { min-height: 240px; }}" +
            ".cleverRow {  width: 100%;  overflow: hidden; } .leftColumn, .rightColumn {text-align:center;  float: left;  width: 40%;  margin: 0 0%;  background: #00BCD4; padding:6 0px;}" +
            ".cleverButton {text-align:center; color:#FFFFFF !important; }" + //Navigation buttons (next, previous article)
            "</style>";

        private static string CSS_INFO_SOURCE = "<style type=\"text/css\"> " +
        "body { background-color:#FFFFFF; font-size: #FONT_SIZE#; margin:0px; color: #BBBBBB !important; font-family: #FONT_FAMILY#; width:100%} " +
        "a {color :#555555; font-size: 1.0em; text-decoration:none; font-family: #FONT_FAMILY#;}" +
        "b {font-size: 1.0em; font-family: #FONT_FAMILY#;}" +
        "img {display: block; max-width:100%; border: 0px solid #636363; text-align: center; margin:10px auto; height: auto;} " +
    
        ".feedSourceTitle {font-size: 1.5em; font-weight: 600; color: #00BCD4 !important; line-height: 1.5em; font-family: #FONT_FAMILY#;  } " +
        ".imageHolder {  margin-left: auto;  margin-right: auto;}" +
        ".imageHolder img{ width:100%;  height:100%; margin:0px;}" +
        ".imageHolder .caption { display:none; position: absolute; width:100%; bottom: 0px; left: 0px; color: #ffffff; background: #000000; text-align:left; font-weight:bold; opacity:0.7; padding-left: 5px; padding-right: 5px; padding-top:5px; padding-bottom:5px;}" +
        "h1 {font-size: 1.4em; font-weight: 700; color: #000000 !important; line-height: 1.5em; font-family: #FONT_FAMILY#;} " +
        "h2 {font-size: 1.3em; font-weight: 600; color: #000000 !important; line-height: 1.3em; font-family: #FONT_FAMILY#;} " +
        "h3 {font-size: 1.2em; font-weight: 600; color: #000000 !important; line-height: 1.2em; font-family: #FONT_FAMILY#;} " +
        "#authorLine {color: #666666; font-size: 1.0em; line-height:  1.5em; margin-bottom:10px; font-weight:400; font-family: #FONT_FAMILY#;} " +
        "blockquote { font-size: 1.0em; line-height:  1.3em; margin-top: 10px; margin-bottom: 10px; font-family: #FONT_FAMILY#; color:#555555 !important; font-weight:800;} " +
        "blockquote p{ font-size: 1.0em; line-height:  1.3em; margin-top: 10px; margin-bottom: 10px; font-family: #FONT_FAMILY#; color:#555555 !important; font-weight:800;} " +
        "p { font-size: 1.0em; line-height: 1.5em; margin-top: 20px; margin-bottom: 20px; font-family: #FONT_FAMILY#; color:#555555 !important; } " +
        "span { font-size: 1.0em; line-height: 1.3em; margin-top: 20px; margin-bottom: 20px; font-family: #FONT_FAMILY#; color:#555555 !important; } " +
        "div { max-width:100% !important; font-size: 1.0em; line-height: 1.3em; font-family: #FONT_FAMILY#;} " +
        "#content  {color:#555555 !important; margin: 12px 12px; font-size: 1em; line-height: 1.3em; word-wrap: break-word; font-family: #FONT_FAMILY#; text-align:left; text-justify:inter-word;s} " +
        "@media all and (max-width: 799px) and (min-width: 320px) { #content { width: 90%; }}" +
        "@media all and (max-width: 2599px) and (min-width: 800px) { #content { width: 720px; }}" +
        "@media all and (max-width: 799px) and (min-width: 320px) { iframe { min-height: 240px; }}" +
        "</style>";


        public string CreateWebPage(float font, string content, string title, string feedSource, string publishDate, string coverPhoto, string author)
        {
            var fontSize = font.ToString(NumberFormatInfo.InvariantInfo);

            if (content != null)
                content = content.Replace("http:http:", "http:");

            feedSource = feedSource.ToUpper();

            bool hasMainImage = true;

            if (!string.IsNullOrEmpty(coverPhoto) && !string.IsNullOrEmpty(content))
                content = content.Replace(coverPhoto, "");
            else
                hasMainImage = false;

            if (!(coverPhoto.Contains("http") || string.IsNullOrEmpty(coverPhoto)))
                coverPhoto = string.Format("http://{0}/{1}", feedSource, coverPhoto);

            var imageLine = hasMainImage ? "<div class=\"imageHolder\" > <img src=" + coverPhoto + " /> <div class=\"caption\">" + title + "</div> </div>" : string.Empty;

            var authorLine = !string.IsNullOrEmpty(author) ? "Autor: (" + author + ") " : string.Empty;

            var style = CSS_SOURCE.Replace("#FONT_SIZE#", string.Format("{0}em", fontSize));
            style = style.Replace("#FONT_FAMILY#", string.Format("{0}", "Verdana"));

            var result = @"<! DOCTYPE html> <html>" +
                "<head>" +
                "<title>Some</title>" + style +
                "<meta name='viewport' content='width=device-width, height=device-height, user-scalable=no'>" +
                "<script type='text/javascript' >" +
                   "function SetFontSize(fontSize) { if (document.body.style.fontSize == '') { document.body.style.fontSize = '1.0em'; } document.body.style.fontSize = fontSize + 'em'; }" +
                   "function NextArticle() {window.external.notify('NextArticle')}" +
                   "function PreviousArticle() {window.external.notify('PreviousArticle')}" +
                   "function OpenImage(imageSrc) {window.external.notify('src='+imageSrc)}" +
                   "function LoadCompleted() {window.external.notify('LoadCompleted')}" +
                   "</script>" +
                    "<script type='text/javascript' src='http://code.jquery.com/jquery-1.7.js'></script>" +
                    "<script type='text/javascript' src='http://hammerjs.github.io/dist/hammer.min.js'></script>" +
                    "<script>" +
                    "$(window).load(function() { " +
                        "window.external.notify('Ready');" +
                        "var hammerOptions = { swipeVelocityX: 0.1, swipeVelocityY: 0.1 };" +
                        "Hammer(content, hammerOptions).on('swipeleft', function(event) { NextArticle(); });" +
                        "Hammer(content, hammerOptions).on('swiperight', function(event) { PreviousArticle(); });" +
                        "$(window).on('click','img', function(event) { OpenImage($(this).attr('src')); })" +
                        " }); </script> " +
                    "</head>" + "<body><div>"
                    + imageLine +
                    "<div id='content'>" +
                    "<div class='feedSourceTitle'>" + feedSource + "</div>" +
                    "<h1>" + title + "</h1>" +
                    "<div id='authorLine'>" + authorLine + " " + publishDate + "</div>" +
                    content +
                    "<br/><br/>" +
                    //"<div class='cleverRow'>" +
                    //    "<div class='leftColumn'  onclick='PreviousArticle()'><span class='cleverButton'>Poprzedni</span></div>" +
                    //    "<div class='rightColumn'  onclick='NextArticle()'><span class='cleverButton'>Następny</span></div>" +
                    //"</div>" +
                    "<br/><br/></div></div>" +
                    "</body>" +
                    "</html>";

            result = result.Replace("frameborder=\"0\"", "");

            return result;
        }

        public string CreateWebPagPhotoInfo(float font, string title, string coverPhoto)
        {
            bool hasMainImage = true;

            var imageLine = hasMainImage ? "<div class=\"imageHolder\" > <img src=" + coverPhoto + " />  </div>" : string.Empty;

            return CreateWebPagInfo(font, title, imageLine);
        }

        public string CreateWebPagVideoInfo(float font, string title, string videoItem)
        {
            //videoItem = "<iframe width=\"550\" height=\"315\" src=\"http//www.youtube.com/embed/YykjpeuMNEk\" frameborder=\"0\" allowfullscreen></iframe>";

            return CreateWebPagInfo(font, title, videoItem);
        }

        public string CreateWebPagRegularInfo(float font, string title, string item)
        {
            return CreateWebPagInfo(font, title, string.Format("<div id=\"content\"><p>{0}</p></div>", title));
        }

        private string CreateWebPagInfo(float font, string title, string coverItem)
        {
            var fontSize = font.ToString(NumberFormatInfo.InvariantInfo);

            var mainElement = coverItem;

            var style = CSS_INFO_SOURCE.Replace("#FONT_SIZE#", string.Format("{0}em", fontSize));
            style = style.Replace("#FONT_FAMILY#", string.Format("{0}", "Verdana"));

            var result = @"<! DOCTYPE html> <html>" +
                "<head>" +
                "<title>Some</title>" + style +
                "<meta name='viewport' content='width=480px, height=320px, user-scalable=no'>" +
                "<meta http-equiv=\"Content-Type\" content=\"text/html,charset=utf-8\">"+
                     "<script type='text/javascript' >" +
                   "function SetFontSize(fontSize) { if (document.body.style.fontSize == '') { document.body.style.fontSize = '1.0em'; } document.body.style.fontSize = fontSize + 'em'; }" +
                   "function NextArticle() {window.external.notify('NextArticle')}" +
                   "function PreviousArticle() {window.external.notify('PreviousArticle')}" +
                   "function OpenImage(imageSrc) {window.external.notify('src='+imageSrc)}" +
                   "function LoadCompleted() {window.external.notify('LoadCompleted')}" +
                   "</script>" +
                    "<script type='text/javascript' src='http://code.jquery.com/jquery-1.7.js'></script>" +
                    "<script type='text/javascript' src='http://hammerjs.github.io/dist/hammer.min.js'></script>" +
                    "<script>" +
                    "$(window).load(function() { " +
                        "window.external.notify('Ready');" +
                        "var hammerOptions = { swipeVelocityX: 0.1, swipeVelocityY: 0.1 };" +
                        "Hammer(content, hammerOptions).on('swipeleft', function(event) { NextArticle(); });" +
                        "Hammer(content, hammerOptions).on('swiperight', function(event) { PreviousArticle(); });" +
                        "$(window).on('click','img', function(event) { OpenImage($(this).attr('src')); })" +
                        " }); </script> " +
                    "</head>" + "<body><div>"
                    //+ title +
                    //"<br/>"
                    + mainElement +
                    "</div>" +
                    "</body>" +
                    "</html>";

            result = result.Replace("frameborder=\"0\"", "");

            return result;
        }

    }
}
