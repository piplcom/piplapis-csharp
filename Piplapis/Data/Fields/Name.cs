using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Name of a person.
     */
    public class Name : Field
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("middle")]
        public string Middle { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("@type")]
        private string type;

        private static readonly HashSet<string> types = new HashSet<string> { "present", "maiden", "former", "alias" };
        private static readonly string myName = typeof(Name).Name;
        public string Type
        {
            set
            {
                ValidateType(value, types, myName);
                this.type = value;
            }
            get { return type; }
        }

        /**
         * @param prefix
         *            Name prefix
         * @param first
         *            First name
         * @param Middle
         *            Middle name
         * @param last
         *            Last name
         * @param suffix
         *            Name suffix
         * @param raw
         *            `raw` is an unparsed name like "Eric T Van Cartman", usefull
         *            when you want to search by name and don't want to work hard to
         *            parse it. Note that in response data there's never name.Raw,
         *            the names in the response are always parsed, this is only for
         *            querying with an unparsed name.
         * @param type
         *            Type is one of "present", "maiden", "former", "alias".
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the First
         *            time Pipl's crawlers found this data on the page.
         */
        public Name(string prefix = null, string first = null, string middle = null, string last = null,
                string suffix = null, string raw = null, string type = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Prefix = prefix;
            this.First = first;
            this.Middle = middle;
            this.Last = last;
            this.Suffix = suffix;
            this.Raw = raw;
            this.Type = type;
        }

        [JsonProperty("display")]
        public string Display
        {
            get
            {
                List<string> vals = new List<string>();
                if (!String.IsNullOrEmpty(Prefix)) vals.Add(Prefix);
                if (!String.IsNullOrEmpty(Raw)) vals.Add(Raw);
                else
                {
                    if (!String.IsNullOrEmpty(First)) vals.Add(First);
                    if (!String.IsNullOrEmpty(Middle)) vals.Add(Middle);
                    if (!String.IsNullOrEmpty(Last)) vals.Add(Last);
                }
                if (!String.IsNullOrEmpty(Suffix)) vals.Add(Suffix);
                return String.Join(" ", vals);
            }
        }

        private static Regex nonAbc = new Regex("[^A-Za-z]");

        /**
         * A bool value that indicates whether the name is a valid name to search
         * by.
         * 
         * @return bool
         */
        [JsonIgnore]
        public override bool IsSearchable
        {
            get
            {
                return ((!String.IsNullOrEmpty(First) && nonAbc.Replace(First, string.Empty).Length >= 2) &&
                        (!String.IsNullOrEmpty(Last) && nonAbc.Replace(Last, string.Empty).Length >= 2)) ||
                        (!String.IsNullOrEmpty(Raw) && nonAbc.Replace(Raw, string.Empty).Length >= 4);
            }
        }
    }
}
