using System.Collections.Generic;
using Newtonsoft.Json;

namespace API.Plaud.NET.Models.Responses
{
    public class ResponseListRecordings
    {
        [JsonProperty("status")]
        public int? Status { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data_file_total")]
        public int? DataFileTotal { get; set; }

        [JsonProperty("data_file_list")]
        public List<DataFileList> DataFileList { get; set; }
    }
}