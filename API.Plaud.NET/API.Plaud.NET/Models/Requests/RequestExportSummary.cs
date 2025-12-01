using Newtonsoft.Json;

namespace API.Plaud.NET.Models.Requests
{
    public class RequestExportSummary
    {
        [JsonProperty("file_id")]
        public string FileId { get; set; }

        [JsonProperty("prompt_type")]
        public string PromptType { get; set; }

        [JsonProperty("to_format")]
        public string ToFormat { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("create_time")]
        public string CreateTime { get; set; }

        [JsonProperty("with_speaker")]
        public int? WithSpeaker { get; set; }

        [JsonProperty("with_timestamp")]
        public int? WithTimestamp { get; set; }

        // Newer API requires summary id for some export operations
        [JsonProperty("summaryid")]
        public string SummaryId { get; set; }

        [JsonProperty("summary_content")]
        public string SummaryContent { get; set; }
    }
}