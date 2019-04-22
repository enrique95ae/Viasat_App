using Newtonsoft.Json;
using System.Collections.Generic;

namespace UserType
{

    public class UserModel
    {
        [JsonProperty("user_name")]
        public string UserName { get; set; }

        //[JsonProperty("user_last")]
        //public string UserLast { get; set; }

        [JsonProperty("permission_level")]
        public string PermissionLevel { get; set; }

        [JsonProperty("notes")]
        public string[] PersonalNotesList {get; set;}

        [JsonProperty("history")]
        public string[] itemsHistory { get; set; }

        [JsonProperty("favorites")]
        public string[] favorites { get; set; }
    }
}