using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;


namespace Pipl.APIs.Utils
{
    /**
     * The TolerantStringEnumConverter is a wrapper around the StringEnumConverter designed to handle gracefully with unknown enum values. 
     */     
    public class TolerantStringEnumConverter: StringEnumConverter
    {
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
