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
    public enum RelationshipTypes
    {
        [EnumMember(Value = "friend")]
        Friend,
        [EnumMember(Value = "family")]
        Family,
        [EnumMember(Value = "work")]
        Work,
        [EnumMember(Value = "other")]
        Other
    }
}
