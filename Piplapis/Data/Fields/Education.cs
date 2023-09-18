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

        [JsonProperty("display")]
        public string Display { get; private set; }

        /**
         * @param validSince `validSince` is a <code>DateTime</code> object, it's the first time Pipl's
         *                   crawlers found this data on the page.
         * @param degree     degree
         * @param school     school
         * @param dateRange  `dateRange` is A <code>DateRange</code> object (Pipl.APIs.Data.Fields.DateRange),
         *                   that's the time the person was studying.
         */
        public Education(string degree = null, string school = null, DateRange dateRange = null, string? validSince = null)
            : base(validSince)
        {
            this.Degree = degree;
            this.School = school;
            this.DateRange = dateRange;
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
