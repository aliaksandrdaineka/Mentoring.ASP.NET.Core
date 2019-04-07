using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebsite.Web.Helpers
{
    [HtmlTargetElement("a", Attributes = "northwind-id")]
    public class NorthwindImageLinkTagHelper : TagHelper
    {
        public int NorthwindId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("href", $"/images/{NorthwindId}");
        }
    }
}
