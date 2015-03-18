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
    public enum EthnicityTypes
    {
        [EnumMember(Value = "white")]
        White,
        [EnumMember(Value = "black")]
        Black,
        [EnumMember(Value = "american_indian")]
        AmericanIndian,
        [EnumMember(Value = "alaska_native")]
        AlaskaNative,
        [EnumMember(Value = "asian_indian")]
        AsianIndian,
        [EnumMember(Value = "chinese")]
        Chinese,
        [EnumMember(Value = "filipino")]
        Filipino,
        [EnumMember(Value = "other_asian")]
        OtherAsian,
        [EnumMember(Value = "japanese")]
        Japanese,
        [EnumMember(Value = "korean")]
        Korean,
        [EnumMember(Value = "vietnamese")]
        Vietnamese,
        [EnumMember(Value = "native_hawaiian")]
        NativeHawaiian,
        [EnumMember(Value = "guamanian")]
        Guamanian,
        [EnumMember(Value = "chamorro")]
        Chamorro,
        [EnumMember(Value = "samoan")]
        Samoan,
        [EnumMember(Value = "other_pacific_islander")]
        OtherPacificIslander,
        [EnumMember(Value = "other")]
        Other
    }
}
