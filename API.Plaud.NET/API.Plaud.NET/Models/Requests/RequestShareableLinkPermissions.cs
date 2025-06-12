using Newtonsoft.Json;

namespace API.Plaud.NET.Models.Requests
{
    public class RequestShareableLinkPermissions
    {
        [JsonProperty("is_audio")]
        public int? IsAudio { get; set; }

        [JsonProperty("is_trans")]
        public int? IsTrans { get; set; }

        [JsonProperty("is_ai_content")]
        public int? IsAiContent { get; set; }

        [JsonProperty("is_mindmap")]
        public int? IsMindmap { get; set; }
    }
}