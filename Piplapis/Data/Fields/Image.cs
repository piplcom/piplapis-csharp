using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * A URL of an image of a person.
     */
    public class Image : Field
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        /**
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         * @param url
         *            url
         */
        public Image(string url = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Url = url;
        }

        [JsonIgnore]
        public bool IsValidUrl
        {
            get
            {
                return !String.IsNullOrEmpty(Url) && Utils.IsValidUrl(this.Url);
            }
        }

    }
}
