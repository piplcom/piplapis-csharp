using Newtonsoft.Json;

namespace Pipl.APIs.Data.Containers
{
    public class AvailableData
    {
        [JsonProperty("basic")]
        public FieldCount Basic { get; set; }

        [JsonProperty("premium")]
        public FieldCount Premium { get; set; }
    }
}
