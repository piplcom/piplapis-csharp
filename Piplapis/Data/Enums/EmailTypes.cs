using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pipl.APIs.Utils;

namespace Pipl.APIs.Data.Enums
{
    [JsonConverter(typeof(TolerantStringEnumConverter))]
    public enum EmailTypes
    {
        [EnumMember(Value = "personal")]
        Personal,
        [EnumMember(Value = "work")]
        Work
    }
}
