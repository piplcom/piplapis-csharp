using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pipl.APIs.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SourceCategories
    {
        [EnumMember(Value = "background_reports")]
        BackgroundReports,
        [EnumMember(Value = "contact_details")]
        ContactDetails,
        [EnumMember(Value = "email_address")]
        EmailAddress,
        [EnumMember(Value = "media")]
        Media,
        [EnumMember(Value = "personal_profiles")]
        PersonalProfiles,
        [EnumMember(Value = "professional_and_business")]
        ProfessionalAndBusiness,
        [EnumMember(Value = "public_records")]
        PublicRecords,
        [EnumMember(Value = "publications")]
        Publications,
        [EnumMember(Value = "school_and_classmates")]
        SchoolAndClassmates,
        [EnumMember(Value = "web_pages")]
        WebPages
    }
}
