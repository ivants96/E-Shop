using E_Shop.Classes;
using E_Shop.Data.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent RenderFlashMessages(this IHtmlHelper helper)
        {
            List<FlashMessage> messageList = helper.ViewContext.TempData.DeserializeToObject<List<FlashMessage>>("Messages");

            var html = new HtmlContentBuilder();

            //Iterate over messages in messaegList
            foreach (var msg in messageList)
            {
                TagBuilder container = new TagBuilder("div");
                container.AddCssClass($"alert alert-{msg.Type.ToString().ToLower()}");
                container.InnerHtml.SetContent(msg.Message);

                html.AppendHtml(container);
            }
            return html;
        }


        public static IHtmlContent RenderCategories(this IHtmlHelper helper, IEnumerable<Category> categories, string parentUrl = "")
        {
            TagBuilder div = new TagBuilder("div");

            foreach (var category in categories)
            {
                var id = category.Title;
                string url = parentUrl + "/" + category.Url;

                if (category.ChildCategories.Count > 0)
                {
                    TagBuilder h6 = new TagBuilder("h6");
                    TagBuilder parentCategoryAnchorTag = new TagBuilder("a");
                    TagBuilder ul = new TagBuilder("ul");


                    h6.InnerHtml.AppendHtml(parentCategoryAnchorTag);

                    parentCategoryAnchorTag.AddCssClass("text-danger");
                    parentCategoryAnchorTag.InnerHtml.SetContent(category.Title);
                    parentCategoryAnchorTag.Attributes.Add("role", "button");
                    parentCategoryAnchorTag.Attributes.Add("href", "#" + id.Split(" ").First());
                    parentCategoryAnchorTag.Attributes.Add("data-toggle", "collapse");
                    parentCategoryAnchorTag.Attributes.Add("aria-expanded", "false");
                    parentCategoryAnchorTag.Attributes.Add("aria-controls", id.Split(" ").First());

                    ul.AddCssClass("list-inline collapse ml-2");
                    ul.Attributes.Add("id", id.Split(" ").First());

                    foreach (var childCategory in category.ChildCategories)
                    {
                        TagBuilder li = new TagBuilder("li");
                        TagBuilder anchorTag = new TagBuilder("a");

                        anchorTag.InnerHtml.SetContent(childCategory.Title);
                        anchorTag.AddCssClass("text-info");

                        li.InnerHtml.AppendHtml(anchorTag);
                        ul.InnerHtml.AppendHtml(li);
                    }

                    div.InnerHtml.AppendHtml(h6);
                    div.InnerHtml.AppendHtml(ul);

                }

            }
            return new HtmlContentBuilder().AppendHtml(div);
        }







    }
}
