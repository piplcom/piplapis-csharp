using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * An ID associated with a person.
     * <p/>
     * The ID is a string that's used by the site to uniquely identify a person,
     * it's guaranteed that in the site this string identifies exactly one person.
     */
    public class UserID : Field
    {
        private static Regex ValidateSearchable = new Regex("\\S+@\\S+");

        [JsonProperty("content")]
        public string Content { get; set; }

        public UserID(string content = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Content = content;
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(this.Content))
                return "";
            return this.Content;
        }

        [JsonIgnore]
        public override bool IsSearchable
        {
            get
            {
                return !String.IsNullOrEmpty(this.Content) && ValidateSearchable.IsMatch(this.Content);
            }
        }
    }
}
