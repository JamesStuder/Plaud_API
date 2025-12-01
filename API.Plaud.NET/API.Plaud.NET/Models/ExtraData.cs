using Newtonsoft.Json;

namespace API.Plaud.NET.Models
{
    public class ExtraData
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("tranConfig")]
        public TranConfig TranConfig { get; set; }

        [JsonProperty("aiContentFrom")]
        public AiContentFrom AiContentFrom { get; set; }

        [JsonProperty("aiContentHeader")]
        public AiContentHeader AiContentHeader { get; set; }

        [JsonProperty("task_id_info")]
        public TaskIdInfo TaskIdInfo { get; set; }
    }
}