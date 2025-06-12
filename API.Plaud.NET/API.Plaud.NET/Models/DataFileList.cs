using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Plaud.NET.Models
{
    public class DataFileList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }

        [JsonProperty("filesize")]
        public int? Filesize { get; set; }

        [JsonProperty("filetype")]
        public string Filetype { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("file_md5")]
        public string FileMd5 { get; set; }

        [JsonProperty("ori_ready")]
        public bool? OriReady { get; set; }

        [JsonProperty("version")]
        public int? Version { get; set; }

        [JsonProperty("version_ms")]
        public long? VersionMs { get; set; }

        [JsonProperty("edit_time")]
        public int? EditTime { get; set; }

        [JsonProperty("edit_from")]
        public string EditFrom { get; set; }

        [JsonProperty("is_trash")]
        public bool? IsTrash { get; set; }

        [JsonProperty("start_time")]
        public long StartTime { get; set; }

        [JsonProperty("end_time")]
        public long EndTime { get; set; }

        [JsonProperty("duration")]
        public int? Duration { get; set; }

        [JsonProperty("timezone")]
        public int? Timezone { get; set; }

        [JsonProperty("zonemins")]
        public int? Zonemins { get; set; }

        [JsonProperty("scene")]
        public int? Scene { get; set; }

        [JsonProperty("filetag_id_list")]
        public List<string> FiletagIdList { get; set; }

        [JsonProperty("is_trans")]
        public bool? IsTrans { get; set; }

        [JsonProperty("is_summary")]
        public bool? IsSummary { get; set; }

        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }

        [JsonProperty("session_id")]
        public int? SessionId { get; set; }

        [JsonProperty("channel")]
        public int? Channel { get; set; }

        [JsonProperty("ori_fullname")]
        public string OriFullname { get; set; }

        [JsonProperty("ori_location")]
        public string OriLocation { get; set; }

        [JsonProperty("trans_result")]
        public List<TransContent> TransResult { get; set; }

        [JsonProperty("ai_content")]
        public string AiContent { get; set; }

        [JsonProperty("extra_data")]
        public ExtraData ExtraData { get; set; }
    }
}