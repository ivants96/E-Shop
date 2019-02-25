using E_Shop.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Classes
{
    public class ExceptionsToMessageFilterAttribute : ActionFilterAttribute
    {

        public string defaultAction;
        public string defaultController;
        public Type ExceptionType { get; private set; }

        public ExceptionsToMessageFilterAttribute() : this(typeof(Exception), "Index", "Home") { }

        public ExceptionsToMessageFilterAttribute(string defaultAction, string defaultController) : this(typeof(Exception), defaultAction, defaultController) { }

        public ExceptionsToMessageFilterAttribute(Type exceptionType, string defaultAction, string defaultController)
        {
            ExceptionType = exceptionType;
            this.defaultAction = defaultAction;
            this.defaultController = defaultController;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null) { return; }

            if (ExceptionType.IsAssignableFrom(context.Exception.GetType()))
            {
                ((Controller)context.Controller).AddDebugMessage(context.Exception);

                string referrer = context.HttpContext.Request.Headers["Referer"];
                if (!string.IsNullOrEmpty(referrer)) { context.Result = new RedirectResult(referrer); }
                else
                {
                    context.Result = new RedirectToActionResult(defaultAction, defaultController, null);
                }

                context.ExceptionHandled = true;
            }
        }
    }
}
