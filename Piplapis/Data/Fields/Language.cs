using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * A language of a person.
     */
    public class Language : Field
    {
        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("display")]
        public string Display { get; private set; }

        /**
         * @param languageCode
         *            `LanguageCode` is 2 letters Language code (like "US") 
         * @param region
         *            `Region` is 2 letters country code (like "GB") 
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         */
        public Language(string languageCode = null, string region = null, 
            DateTime? validSince = null)
            : base(validSince)
        {
            this.LanguageCode = languageCode;
            this.Region = region;
        }
    
        public override string ToString()
        {
            return Display;
        }
    }
}
