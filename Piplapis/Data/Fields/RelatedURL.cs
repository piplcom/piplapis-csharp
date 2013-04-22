using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * IMPORTANT: This URL is NOT the origin of the data about the person, it's just
     * an extra piece of information available on him.
     */
    public class RelatedURL : Field
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        private static readonly HashSet<string> types = new HashSet<string> { "personal", "work", "blog" };
        private static readonly string myName = typeof(RelatedURL).Name;

        [JsonProperty("@type")]
        private string type;
        public string Type
        {
            set
            {
                ValidateType(value, types, myName);
                this.type = value;
            }
            get { return this.type; }
        }

        /**
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         * @param content
         *            `content` is the URL address itself.
         * @param type
         *            type is a string and is one of "personal", "work", "blog".
         */
        public RelatedURL(string content = null, string type = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Content = content;
            this.Type = type;
        }

        [JsonIgnore]
        public bool IsValidUrl
        {
            get
            {
                return !String.IsNullOrEmpty(Content) && Utils.IsValidUrl(Content);
            }
        }

    }
}
