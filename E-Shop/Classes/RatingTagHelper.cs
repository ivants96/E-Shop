using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Classes
{
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
            
            for (int i = 0; i < value; i++)
            {
                var builder = new TagBuilder("span");
                builder.AddCssClass("fa fa-star");
                builder.Attributes.Add("style", "color:#ffc94b;");
                output.Content.AppendHtml(builder);
            }
            for (int i = 0; i < 5 - value; i++)
            {
                var builder = new TagBuilder("span");
                builder.AddCssClass("fa fa-star-o");
                output.Content.AppendHtml(builder);
            }
        }


    }
}
