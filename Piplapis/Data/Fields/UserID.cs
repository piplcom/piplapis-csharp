using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        [JsonProperty("content")]
        public string Content { get; set; }

        public UserID(string content = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Content = content;
        }
    }
}
