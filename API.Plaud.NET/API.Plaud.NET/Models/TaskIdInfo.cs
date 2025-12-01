using Newtonsoft.Json;

namespace API.Plaud.NET.Models
{
    public class TaskIdInfo
    {
        [JsonProperty("summary_id")]
        public string SummaryId { get; set; }

        [JsonProperty("trans_task_id")]
        public string TransTaskId { get; set; }

        [JsonProperty("outline_task_id")]
        public string OutlineTaskId { get; set; }
    }
}
