using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Reflection;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Base class of all data fields, made only for inheritance.
     */
    public class Field
    {
        [JsonProperty("@valid_since")]
        public DateTime? ValidSince { get; set; }

        [JsonProperty("@inferred")]
        public bool? Inferred { get; set; }

        public Field(DateTime? validSince = null, bool? inferred = null)
        {
            this.ValidSince = validSince;
            this.Inferred = inferred;
        }

        /**
	     * method to check if a field is searchable
	     * 
	     * @return bool
	     */
        [JsonIgnore]
        public virtual bool IsSearchable
        {
            get
            {
                return true;
            }
        }

        public String Repr()
        {
            List<String> list_of_strings = new List<String>();
            var goodProperties = from property in this.GetType().GetProperties() where (property.CanWrite && property.GetValue(this, null) != null) select property;

            foreach (var prop in goodProperties)
            {
                String value = prop.GetValue(this, null).ToString();
                value = (prop.PropertyType == typeof(String)) ? '"' + value + '"' : value;
                list_of_strings.Add(prop.Name + ": " + value);
            }
            return this.GetType().Name + "(" + String.Join(", ", list_of_strings) + ")";
        }
    }
}