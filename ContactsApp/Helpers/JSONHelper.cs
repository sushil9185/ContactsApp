using System.Web.Script.Serialization;
using System.Data;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace ContactsApp.Helpers
{
    public static class JSONHelper
    {
        #region Public extension methods.
        /// <summary>
        /// Extened method of object class, Converts an object to a json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                //return serializer.Serialize(obj);
                return JsonConvert.SerializeObject(obj, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
    }
}