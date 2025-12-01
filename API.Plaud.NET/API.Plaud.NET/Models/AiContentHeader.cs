using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Plaud.NET.Models
{
    public class AiContentHeader
    {
        [JsonProperty("headline")]
        public string Headline { get; set; }

        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }

        // Optional in newer API responses
        [JsonProperty("summary_id")]
        public string SummaryId { get; set; }

        // Optional: language code may appear in newer responses
        [JsonProperty("language_code")]
        public string LanguageCode { get; set; }

        [JsonProperty("industry_category")]
        public string IndustryCategory { get; set; }

        [JsonProperty("recommend_questions")]
        public List<RecommendQuestion> RecommendQuestions { get; set; }
    }
}