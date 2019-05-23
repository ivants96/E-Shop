using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Business.Extensions
{
    public static class CookieHelperExtensions
    {
        public static void SetCookie(this HttpContext context, string key, string value, TimeSpan expires)
        {
            // update existing cookie if exists else create new cookie
            var cookieOptions = new CookieOptions() { Expires = DateTime.Now.Add(expires), HttpOnly = true };
            if(context.Request.Cookies[key] != null)
            {
                string oldCookie = context.Request.Cookies[key];
                context.Response.Cookies.Append(key, oldCookie, cookieOptions);
            }
            else
            {
                cookieOptions.Expires = DateTime.Now.Add(expires);
                context.Response.Cookies.Append(key, value, cookieOptions);
            }
        }

        public static string GetCookie(this HttpContext context, string key) => context.Request.Cookies[key] ?? string.Empty;
       
     
        

    }
}
