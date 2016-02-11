using Newtonsoft.Json;

namespace Pipl.APIs.Data.Containers
{
    /**
     * A FieldCount object is a summary of the data within an API response.
     */
    public class FieldCount
    {

        [JsonProperty("addresses")]
        public int Addresses { get; set; }

        [JsonProperty("ethnicities")]
        public int Ethnicities { get; set; }

        [JsonProperty("emails")]
        public int Emails { get; set; }

        [JsonProperty("dobs")]
        public int Dobs { get; set; }

        [JsonProperty("genders")]
        public int Genders { get; set; }

        [JsonProperty("user_ids")]
        public int UserIDs { get; set; }

        [JsonProperty("educations")]
        public int Educations { get; set; }

        [JsonProperty("jobs")]
        public int Jobs { get; set; }

        [JsonProperty("languages")]
        public int Languages { get; set; }

        [JsonProperty("origin_countries")]
        public int OriginCountries { get; set; }

        [JsonProperty("names")]
        public int Names { get; set; }

        [JsonProperty("phones")]
        public int Phones { get; set; }

        [JsonProperty("relationships")]
        public int Relationships { get; set; }

        [JsonProperty("social_profiles")]
        public int SocialProfiles { get; set; }

        [JsonProperty("images")]
        public int Images { get; set; }

    }
}
