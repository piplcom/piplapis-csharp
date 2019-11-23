﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Pipl.APIs.Data.Enums;
using Pipl.APIs.Utils;

namespace Pipl.APIs.Data.Fields
{
    /**
     * A phone number of a person.
     */
    public class Phone : Field
    {
        [JsonProperty("country_code")]
        public int? CountryCode { get; set; }

        [JsonProperty("number")]
        public long? Number { get; set; }

        [JsonProperty("extension")]
        public int? Extension { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set;}

        [JsonProperty("display")]
        public string Display { get; private set; }

        [JsonProperty("display_international")]
        public string DisplayInternational { get; private set; }

        [JsonProperty("@type")]
        public PhoneTypes? Type { get; set; }

        /**
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         * @param countryCode
         *            countryCode
         * @param number
         *            number
         * @param extension
         *            extension
         * @param type
         *            type is one of "mobile", "home_phone", "home_fax",
         *            "work_phone" , "pager".
         * @param raw
         *            `raw` is an unparsed phone
         */
        public Phone(int? countryCode = null, long? number = null, int? extension = null,
                PhoneTypes? type = null, string raw = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.CountryCode = countryCode;
            this.Number = number;
            this.Extension = extension;
            this.Type = type;
            this.Raw = raw;
        }

        /**
         * A bool value that indicates whether the address is a valid address to
         * search by.
         * 
         * @return bool
         */
        [JsonIgnore]
        public override bool IsSearchable
        {
            get
            {

                if (!string.IsNullOrEmpty(Raw)) return true;
                if (CountryCode == null && Number >= 99999999) return true;
                if (CountryCode != null && CountryCode>0 && CountryCode <=999 && Number >= 999999) return true;
                return false;  
                   
            }
        }

        /**
         * Sets the Raw number to be the input text 
         */
        public static Phone FromText(string text)
        {
            var res = new Phone
            {
                Raw = text
            };

            return res;
        }

        public override string ToString()
        {
            string str;
            List<string> vals = new List<string>();

            if (this.CountryCode != null)
                vals.Add(this.CountryCode.ToString());
            if (this.Number != null)
                vals.Add(this.Number.ToString());
            if (this.Extension != null)
                vals.Add(this.Extension.ToString());

            str = String.Join("-", vals);

            vals.Clear();

            if (!String.IsNullOrEmpty(str))
                vals.Add(str);

            if (!String.IsNullOrEmpty(this.Raw))
                vals.Add(this.Raw);
            if (this.Type != null)
                vals.Add(EnumExtensions.JsonEnumName(Type.Value));

            str = String.Join(", ", vals);

            return str;
        }
    }
}
