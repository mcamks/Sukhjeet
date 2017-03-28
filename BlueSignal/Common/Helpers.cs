using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BlueSignal.Common
{
    public static class Helpers
    {
        public static T GetJsonResponse<T>(string jsonResult, string parserString)
        {
            //Check reader has some rows
            try
            {
                //Create object of JObject class and parse the json result
                JObject jsonResponse = JObject.Parse(jsonResult.ToString());
                var objResponse = jsonResponse[parserString];
                if (objResponse != null)
                {
                    return JsonConvert.DeserializeObject<T>(Convert.ToString(objResponse));
                }
                return (T)Activator.CreateInstance(typeof(T));
            }
            catch (Exception ex)
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
        }


        public static string GetShortDateString(this DateTime? obj)
        {
            return obj.HasValue ? obj.Value.ToString("d") : string.Empty;
        }
    }
}