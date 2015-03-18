using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pipl.APIs.Data.Enums;
using Pipl.APIs.Utils;

namespace Pipl.APIs.Data.Fields
{
    /**
    * Ethnicity of a person.
    */
    public class Ethnicity : Field
    {
        private static readonly string myName = typeof(Ethnicity).Name;

        [JsonProperty("content")]
        public EthnicityTypes? Content { get; set; }
        
        /**
         * @param content
         *            Content.
         *            Possible values: White, Black, American Indian, Alaska Native, Asian Indian, Chinese, Filipino, Other Asian, 
         *            Japanese, Korean, Vietnamese, Native Hawaiian, Guamanian, Chamorro, Samoan, Other Pacific Islander, Other
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         */
        public Ethnicity(EthnicityTypes? content = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            if (Content == null)
                return "";
            return EnumExtensions.JsonEnumName(Content.Value);
        }
    }
}
