using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Education information of a person.
     */
    public class Education : Field
    {
        [JsonProperty("degree")]
        public string Degree { get; set; }

        [JsonProperty("school")]
        public string School { get; set; }

        [JsonProperty("date_range")]
        public DateRange DateRange { get; set; }

        /**
         * @param validSince `validSince` is a <code>DateTime</code> object, it's the first time Pipl's
         *                   crawlers found this data on the page.
         * @param degree     degree
         * @param school     school
         * @param dateRange  `dateRange` is A <code>DateRange</code> object (Pipl.APIs.Data.Fields.DateRange),
         *                   that's the time the person was studying.
         */
        public Education(string degree = null, string school = null, DateRange dateRange = null, DateTime? validSince = null)
            : base(validSince)
        {
            this.Degree = degree;
            this.School = school;
            this.DateRange = dateRange;
        }

        [JsonProperty("display")]
        public string Display
        {
            get
            {
                string result = "";
                if (!string.IsNullOrEmpty(this.Degree) && !string.IsNullOrEmpty(this.School))
                {
                    result = this.Degree + " from " + this.School;
                }
                else if (!string.IsNullOrEmpty(this.Degree))
                {
                    result = this.Degree;
                }
                else if (!string.IsNullOrEmpty(this.School))
                {
                    result = this.School;
                }
                if (!string.IsNullOrEmpty(result) && this.DateRange != null)
                {
                    result = result + " (" + this.DateRange.YearsRange.Item1 + "-" + this.DateRange.YearsRange.Item2 + ") ";
                }
                return result.Trim();
            }
        }
    }
}
