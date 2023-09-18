using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * URL. only shown in full profile
     */
    public class URL : Field
    {
        [JsonProperty("@sponsored")]
        public bool? Sponsored { get; set; }

        [JsonProperty("@domain")]
        public string Domain { get; set; }

        [JsonProperty("@name")]
        public string Name { get; set; }

        [JsonProperty("@category")]
        public string Category { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("@source_id")]
        public string? SourceId { get; set; }

        /**
         * @param @sponsered
         *            Sponsered. if false omitted
         * @param @domain
         *            Domain 
         * @param @name
         *            Name 
         * @param @category
         *            Category 
         * @param url
         *            Url
         * @param SourceId
                    @source_id
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         */
        public URL(
            bool? sponsered = null, 
            string domain = null, 
            string name = null,
            string category = null, 
            string url = null,
            string? validSince = null,
            string? SourceId = null
        ): base(validSince){
            this.Sponsored = sponsered;
            this.Domain = domain;
            this.Name = name;
            this.Category = category;
            this.Url = url;
            this.SourceId = SourceId;
        }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(Url))
                return Url;
            if (!String.IsNullOrEmpty(Name))
                return Name;
            return "";
        }
    }
}
