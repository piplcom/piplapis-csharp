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
            DateTime? validSince = null)
            : base(validSince)
        {
            this.Title = title;
            this.Organization = organization;
            this.Industry = industry;
            this.DateRange = dateRange;
        }

        [JsonProperty("diplay")]
        public string Display
        {
            get
            {   
                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Organization))
                    sb.Append(Title).Append(" at ").Append(Organization);
                else
                    // One of them is empty.
                    sb.Append(Title).Append(Organization);

                if (!string.IsNullOrEmpty(Industry))
                {
                    if (DateRange != null)
                    {
                        if (sb.Length == 0)
                        {
                            sb.Append(Industry + " ("
                                      + DateRange.YearsRange.Item1 + "-"
                                      + DateRange.YearsRange.Item2 + ")");
                        }
                        else
                        {
                            sb.Append(" (" + Industry + ", "
                                    + DateRange.YearsRange.Item1 + "-"
                                    + DateRange.YearsRange.Item2 + ")");
                        }
                    }
                    else
                    {
                        if (sb.Length == 0)
                        {
                            sb.Append(Industry);
                        }
                        else
                        {
                            sb.Append(" (" + Industry + ")");
                        }
                    }
                }
                else
                {
                    if (DateRange != null && sb.Length != 0)
                    {
                        sb.Append(" (" + DateRange.YearsRange.Item1 + "-"
                                + DateRange.YearsRange.Item2 + ")");
                    }
                }
                string result = sb.ToString();
                return result;
            }
        }
    }
}
