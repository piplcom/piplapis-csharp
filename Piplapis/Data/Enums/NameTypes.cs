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
    public enum NameTypes
    {
        [EnumMember(Value = "present")]
        Present,
        [EnumMember(Value = "maiden")]
        Maiden,
        [EnumMember(Value = "former")]
        Former,
        [EnumMember(Value = "alias")]
        Alias
    }
}
