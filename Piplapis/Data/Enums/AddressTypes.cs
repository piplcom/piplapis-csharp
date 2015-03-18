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
    public enum AddressTypes
    {
        [EnumMember(Value = "home")]
        Home,
        [EnumMember(Value = "work")]
        Work,
        [EnumMember(Value = "old")]
        Old
    }
}
