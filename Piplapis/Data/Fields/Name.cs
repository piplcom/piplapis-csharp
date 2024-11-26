﻿using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Pipl.APIs.Data.Enums;

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
        public NameTypes? Type { get; set; }

        [JsonProperty("display")]
        public string Display { get; private set; }

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
         *            Type is one of "present", "maiden", "former", "alias", "autogenerated" or "alternative".
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the First
         *            time Pipl's crawlers found this data on the page.
         */
        public Name(string prefix = null, string first = null, string middle = null, string last = null,
                string suffix = null, string raw = null, NameTypes? type = null, string? validSince = null)
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

        public override string ToString()
        {
            return Display;
        }

        private static Regex _nonAbc = new Regex("[\\W\\d]");

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
                return ((!String.IsNullOrEmpty(First) && _nonAbc.Replace(First, string.Empty).Length >= 1) &&
                        (!String.IsNullOrEmpty(Last) && _nonAbc.Replace(Last, string.Empty).Length >= 1)) ||
                        (!String.IsNullOrEmpty(Raw) && _nonAbc.Replace(Raw, string.Empty).Length >= 1);
            }
        }
    }
}
