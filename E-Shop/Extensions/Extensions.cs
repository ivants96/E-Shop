using E_Shop.Business.Managers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Extensions
{
    public static class TempDataExtensions
    {
        public static T DeserializeToObject<T>(this ITempDataDictionary tempData, string key) where T : new()
        {
            string entry = tempData[key]?.ToString();

            T result = (entry == null) ? new T() :
                                         JsonConvert.DeserializeObject<T>(entry);
            return result;
        }

        public static void SerializeObject<T>(this ITempDataDictionary tempData, T obj, string key)
        {
            tempData[key] = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

       
    }



    public static class Extensions
    {
        public static IServiceCollection AddImageProcessing(this IServiceCollection services)
        {
            return ImageManager.ConfigureImageProcessingMiddleWare(services);
        }
    }
}
