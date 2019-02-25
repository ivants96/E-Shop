using E_Shop.Classes;
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
            foreach(var msg in messageList)
            {
                TagBuilder container = new TagBuilder("div");
                container.AddCssClass($"alert alert-{msg.Type.ToString().ToLower()}");
                container.InnerHtml.SetContent(msg.Message);

                html.AppendHtml(container);
            }
            return html;
        }
}
}
