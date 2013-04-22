using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pipl.APIs.Data.Fields;
using Newtonsoft.Json;

namespace Pipl.APIs.Name
{
    /**
     * Helper class for NameAPIResponse, holds an Age range and the estimated
     * percent of people with the name that their Age is within the range.
     */
    public class AgeStats : Field
    {
        [JsonProperty("from_age")]
        public int? FromAge { get; set; }

        [JsonProperty("to_age")]
        public int? ToAge { get; set; }

        [JsonProperty("estimated_percent")]
        public int? EstimatedPercent { get; set; }

        public AgeStats(int? fromAge = null, int? toAge = null, int? estimatedPercent = null)
            : base(default(DateTime))
        {
            this.FromAge = fromAge;
            this.ToAge = toAge;
            this.EstimatedPercent = estimatedPercent;
        }
    }
}
