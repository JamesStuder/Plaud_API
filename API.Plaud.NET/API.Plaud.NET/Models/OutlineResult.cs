using Newtonsoft.Json;

namespace API.Plaud.NET.Models
{
    public class OutlineResult
    {
        [JsonProperty("start_time")]
        public long StartTime { get; set; }

        [JsonProperty("end_time")]
        public long EndTime { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }
    }
}
