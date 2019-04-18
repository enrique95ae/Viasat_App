using Newtonsoft.Json;
using System.Collections.Generic;

namespace ItemType
{

    public class ItemModel
    {
        [JsonProperty("_id")]
        public string id { get; set; }

        [JsonProperty("item_number")]
        public int? item_number { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("part_type")]
        public string part_type { get; set; }

        [JsonProperty("permission_level")]
        public int? permission_level { get; set; }

        [JsonProperty("components")]
        public string components { get; set; }

        [JsonProperty("revision")]
        public int? revision { get; set; }

        //so each id can be displayed in one cell of a list view and can be clicked
        public List<string> componentsIDs { get; set; }
    }

}