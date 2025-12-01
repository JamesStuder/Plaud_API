using Newtonsoft.Json;

namespace API.Plaud.NET.Models
{
    public class TranConfig
    {
        [JsonProperty("llm")]
        public string Llm { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("type_type")]
        public string TypeType { get; set; }

        [JsonProperty("diarization")]
        public int Diarization { get; set; }

        // Newer responses may include created_at timestamp
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
}