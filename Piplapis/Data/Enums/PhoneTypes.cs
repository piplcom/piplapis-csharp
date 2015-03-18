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
    public enum PhoneTypes
    {
        [EnumMember(Value = "mobile")]
        Mobile,
        [EnumMember(Value = "home_phone")]
        HomePhone,
        [EnumMember(Value = "home_fax")]
        HomeFax,
        [EnumMember(Value = "work_phone")]
        WorkPhone,
        [EnumMember(Value = "work_fax")]
        WorkFax,
        [EnumMember(Value = "pager")]
        Pager
    }
}
