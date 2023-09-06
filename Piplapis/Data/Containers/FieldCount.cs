﻿using Newtonsoft.Json;

namespace Pipl.APIs.Data.Containers
{
    /**
     * A FieldCount object is a summary of the data within an API response.
     */
    public class FieldCount
    {


        [JsonProperty("addresses")]
        public int? Addresses { get; set; }
        
        [JsonProperty("ethnicities")]
        public int? Ethnicities { get; set; }
        
        [JsonProperty("emails")]
        public int? Emails { get; set; }

        [JsonProperty("work_emails")]
        public int? WorkEmails { get; set; }

        [JsonProperty("vehicles")]
        public int? Vehicles { get; set; }

        [JsonProperty("usernames")]
        public int? Usernames { get; set; }

        [JsonProperty("personal_emails")]
        public int? PersonalEmails { get; set; }
        
        [JsonProperty("dobs")]
        public int? Dobs { get; set; }
        
        [JsonProperty("genders")]
        public int? Genders { get; set; }
        
        [JsonProperty("user_ids")]
        public int? UserIDs { get; set; }
        
        [JsonProperty("educations")]
        public int? Educations { get; set; }
        
        [JsonProperty("jobs")]
        public int? Jobs { get; set; }
        
        [JsonProperty("languages")]
        public int? Languages { get; set; }
        
        [JsonProperty("origin_countries")]
        public int? OriginCountries { get; set; }
        
        [JsonProperty("names")]
        public int? Names { get; set; }
        
        [JsonProperty("phones")]
        public int? Phones { get; set; }
        
        [JsonProperty("relationships")]
        public int? Relationships { get; set; }
        
        [JsonProperty("social_profiles")]
        public int? SocialProfiles { get; set; }
        
        [JsonProperty("images")]
        public int? Images { get; set; }
        
        [JsonProperty("mobile_phones")]
        public int? MobilePhones { get; set; }
        
        [JsonProperty("landline_phones")]
        public int? LandlinePhones { get; set; }

        [JsonProperty("voip_phones")]
        public int? VoipPhones { get; set; }
    }
}
