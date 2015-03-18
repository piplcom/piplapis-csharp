using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Origin country of a person.
     */
    public class OriginCountry : Field
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        /**
         * @param content
         *            `Content` is 2 letters country code (like "GB") 
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         */
        public OriginCountry(string content = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(this.Content))
                return "";

            if (Utils.Countries.ContainsKey(Content.ToUpper()))
                return Utils.Countries[Content.ToUpper()];

            return "";
        }
    }
}
