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
    }
}
