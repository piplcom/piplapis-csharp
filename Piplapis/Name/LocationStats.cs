using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pipl.APIs.Data.Fields;
using Newtonsoft.Json;

namespace Pipl.APIs.Name
{
    /**
     * Helper class for NameAPIResponse, holds a location and the estimated
     * percent of people with the name that lives in this location.
     * <p/>
     * Note that this class inherits from Address and therefore has the
     * properties locationStats.countryFull(), locationStats.stateFull() and
     * locationStats.Display().
     */
    public class LocationStats : Address
    {
        [JsonProperty("estimated_percent")]
        public int? EstimatedPercent { get; set; }

        public LocationStats(string country = null, string state = null, string city = null, int? estimatedPercent = null) :
            base(country: country, state: state, city: city)
        {
            this.EstimatedPercent = estimatedPercent;
        }
    }
}
