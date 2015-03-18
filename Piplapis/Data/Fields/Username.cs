using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * A Username/screen-name associated with the person.
     * <p/>
     * Note that even though in many sites the Username uniquely identifies one
     * person it's not guaranteed, some sites allow different people to use the same
     * Username.
     */
    public class Username : Field
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        /**
         * Constructor
         * 
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         * @param content
         *            `content` is the Username itself.
         */
        public Username(string content = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Content = content;
        }

        /**
         * A bool value that indicates whether the Username is a valid Username to
         * search by.
         * 
         * @return bool
         */
        [JsonIgnore]
        public override bool IsSearchable
        {
            get
            {
                return Content != null && Regex.Replace(Content, "[^A-Za-z0-9]", "").Length >= 4;
            }
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(this.Content))
                return "";
            return this.Content;
        }
    }
}
