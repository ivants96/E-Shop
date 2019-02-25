using E_Shop.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Extensions
{
    public static class ControllerExtensions
{
        public static void AddFlashMessage(this Controller controller, FlashMessage message)
        {
            List<FlashMessage> list = controller.TempData.DeserializeToObject<List<FlashMessage>>("Messages");

            list.Add(message);
            controller.TempData.SerializeObject(list, "Messages");
        }

        public static void AddFlashMessage(this Controller controller, string message, FlashMessageType messageType)
        {
            controller.AddFlashMessage(new FlashMessage(message, messageType));
        }

        public static void AddDebugMessage(this Controller controller, Exception ex)
        {
            string message = ex.Message;
            if (ex.GetBaseException().Message != message) { message += Environment.NewLine + ex.GetBaseException().Message; }
            AddFlashMessage(controller, new FlashMessage(message, FlashMessageType.Danger));
        }

        public static void AddFlashMessage(this PageModel pageModel, string message, FlashMessageType messageType)
        {
            pageModel.AddFlashMessage(new FlashMessage(message, messageType));
        }

        public static void AddDebugMessage(this PageModel pageModel, Exception ex)
        {
            string message = ex.Message;
            if (ex.GetBaseException().Message != message) { message += Environment.NewLine + ex.GetBaseException().Message; }
            AddFlashMessage(pageModel, new FlashMessage(message, FlashMessageType.Danger));
        }

        public static void AddFlashMessage(this PageModel pageModel, FlashMessage message)
        {
            List<FlashMessage> list = pageModel.TempData.DeserializeToObject<List<FlashMessage>>("Messages");

            list.Add(message);
            pageModel.TempData.SerializeObject(list, "Messages");
        }

    }
}
