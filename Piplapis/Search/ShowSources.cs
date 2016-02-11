using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Pipl.APIs.Search
{
    public enum ShowSources
    {
        [EnumMember(Value="false")]
        False,
        [EnumMember(Value = "true")]
        True,
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "matching")]
        Matching
    }
}
