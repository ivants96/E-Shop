using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Classes
{
    [HtmlTargetElement("rating")]
    public class RatingTagHelper : TagHelper
    {
        private int value = 0;
        public string Value
        {
            get { return value.ToString(); }
            set { this.value = int.Parse(value); }
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.TagMode = TagMode.StartTagAndEndTag;
           
            for (int i = 0; i < value; i++)  // create yellow stars depending on average rating
            {
                var builder = new TagBuilder("span");
                builder.AddCssClass("fa fa-star text-warning");                
                output.Content.AppendHtml(builder);
            }
            for (int i = 0; i < 5 - value; i++) // if average rating isn't 5 add empty stars till there are 5 stars totally 
            {
                var builder = new TagBuilder("span");
                builder.AddCssClass("fa fa-star-o");
                output.Content.AppendHtml(builder);
            }
        }


    }
}
