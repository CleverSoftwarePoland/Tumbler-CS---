using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clever.Win.Tumblr.Services
{
    public interface IPageGenerator
    {
        string CreateWebPage(float font, string content, string title, string feedSource, string publishDate, string coverPhoto, string author);
    }
}
