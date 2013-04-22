using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Name of another person related to this person.
     */
    public class Relationship : Field
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        private static readonly HashSet<string> types = new HashSet<string> { "friend", "family", "work", "other" };
        private static readonly string myName = typeof(Relationship).Name;

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

        [JsonProperty("subtype")]
        public string Subtype { get; set; }

        /**
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         * @param name
         *            name
         * @param type
         *            type is a string and one of "friend", "family", "work",
         *            "other"
         * @param subtype
         *            `subtype` is a string and is not restricted to a specific list
         *            of possible values (for example, if type_ is "family" then
         *            subtype can be "Father", "Mother", "Son" and many other
         *            things).
         */
        public Relationship(Name name = null, string type = null, string subtype = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Name = name;
            this.Type = type;
            this.Subtype = subtype;
        }
    }
}
