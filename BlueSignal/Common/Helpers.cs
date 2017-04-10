using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
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

    public class DynamicContractResolver : DefaultContractResolver
    {
        private readonly List<string> _propertiesNotToSerialize;
        public DynamicContractResolver(string propertiesNotToSerialize = "")
        {
            var list = !string.IsNullOrEmpty(propertiesNotToSerialize) ? propertiesNotToSerialize.Split(',').ToList() : new List<string>();
            _propertiesNotToSerialize = list;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            //// only serializer properties that start with the specified character
            //properties =
            //  properties.Where(p => p.PropertyName.StartsWith(_startingWithChar.ToString())).ToList();

            // only serializer properties that start with the specified character
            properties = properties.Where(p => !_propertiesNotToSerialize.Contains(p.PropertyName.ToLower())).ToList();

            return properties;
        }



    }


    public static class Helpers2
    {
        public static string IsFirstTime()
        {
            return HttpContext.Current.Session["firstTimeCheck"] != null ? Convert.ToString(HttpContext.Current.Session["firstTimeCheck"]) : "0";
        }


        public static string ActiveCssClass
        {
            get
            {
                return HttpContext.Current.Session["ActiveCssClass"] != null ? Convert.ToString(HttpContext.Current.Session["ActiveCssClass"]) : "Dashboard";
            }
        }

    }
}