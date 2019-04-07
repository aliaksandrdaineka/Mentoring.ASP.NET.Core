using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreWebsite.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper helper, int imageId, string linkText)
        {
            return new HtmlString($"<a href='/images/{imageId}'>{linkText}</a>");
        }
    }
}