using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Base class of all data fields, made only for inheritance.
     */
    public class Field
    {
        [JsonProperty("@valid_since")]
        public DateTime? ValidSince { get; set; }

        public Field(DateTime? validSince = null)
        {
            this.ValidSince = validSince;
        }

        protected static void ValidateType(string type, HashSet<string> types, string classType)
        {
            if (!String.IsNullOrEmpty(type) && !types.Contains(type.ToLower()))
            {
                throw new ArgumentException("Invalid Type for " + classType + ": " + type);
            }
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
    }
}
