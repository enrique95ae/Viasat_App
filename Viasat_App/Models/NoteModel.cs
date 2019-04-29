using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace NoteType
{
    public class NoteModel
    {
        [JsonProperty("_id")]
        public string _id { get; set; }

        [JsonProperty("author_id")]
        public string author_id { get; set; }

        [JsonProperty("author")]
        public string author { get; set; }

        [JsonProperty("note")]
        public string note { get; set; }

        [JsonProperty("belongs")]
        public string belongs { get; set; }

        [JsonProperty("date")]
        public string date { get; set; }

    }
}