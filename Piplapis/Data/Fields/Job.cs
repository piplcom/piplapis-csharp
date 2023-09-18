using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Data.Fields
{
    /**
     * Job information of a person.
     */
    public class Job : Field
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("industry")]
        public string Industry { get; set; }

        [JsonProperty("date_range")]
        public DateRange DateRange { get; set; }

        [JsonProperty("display")]
        public string Display { get; private set; }

        /**
         * @param validSince
         *            `validSince` is a <code>DateTime</code> object, it's the first
         *            time Pipl's crawlers found this data on the page.
         * @param title
         *            title
         * @param organization
         *            organization
         * @param industry
         *            industry
         * @param dateRange
         *            `dateRange` is A <code>DateRange</code> object
         *            (Pipl.APIs.Data.Fields.DateRange), that's the time the person
         *            held this job.
         */
        public Job(string title = null, string organization = null, string industry = null, DateRange dateRange = null,
            string? validSince = null)
            : base(validSince)
        {
            this.Title = title;
            this.Organization = organization;
            this.Industry = industry;
            this.DateRange = dateRange;
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
