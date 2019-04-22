using Newtonsoft.Json;
using System.Collections.Generic;

namespace UserType
{
    public class UserModel
    {
        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("user_last")]
        public string UserLast { get; set; }

        [JsonProperty("permission_level")]
        public int PermissionLevel { get; set; }

        [JsonProperty("notes")]
        public List<string> PersonalNotesList {get; set;}

        [JsonProperty("recently_viewed")]
        public List<string> recentlyViewed { get; set; }

        [JsonProperty("favorites")]
        public List<string> favorites { get; set; }

        [JsonProperty("recent_comments")]
        public List<string> recentComments { get; set; }
    }
}