using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace favType
{

    public class FavModel
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("item_id")]
        public string item_id { get; set; }
    }
}