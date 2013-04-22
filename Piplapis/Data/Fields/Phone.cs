using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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

        [JsonProperty("display")]
        public string Display { get; private set; }

        [JsonProperty("display_international")]
        public string DisplayInternational { get; private set; }

        private static readonly HashSet<string> types = new HashSet<string> { "mobile", "home_phone", "home_fax", "work_phone", "work_fax", "pager" };
        private static readonly string myName = typeof(Phone).Name;
        [JsonProperty("@type")]
        private String type;
        public String Type
        {
            set
            {
                ValidateType(value, types, myName);
                this.type = value;
            }
            get { return type; }
        }

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
         */
        public Phone(int? countryCode = null, long? number = null, int? extension = null,
                string type = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.CountryCode = countryCode;
            this.Number = number;
            this.Extension = extension;
            this.Type = type;
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
                return Number != null && (CountryCode == null || CountryCode == 0 || CountryCode == 1);
            }
        }

        private static Regex regexObj = new Regex(@"[^\d]");

        /**
         * Strip `text` from all non-digit chars and return a new Phone object with
         * the number from text.
         * 
         * @param text
         *            String to be parsed
         * @return <code>Phone</code> object example :
         *         <p/>
         *         <blockquote>
         * 
         *         <pre>
         * Phone phone = Phone.FromText("(888) 777-666");
         * </pre>
         * 
         *         </blockquote>
         *         <p/>
         *         phone.number will be - 888777666
         */
        public static Phone FromText(string text)
        {
            long number = Convert.ToInt64(regexObj.Replace(text, string.Empty));
            return new Phone(number: number);
        }
    }
}
