using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        public ObservableCollection<string> PersonalNotesList {get; set;}

        [JsonProperty("recently_viewed")]
        public ObservableCollection<string> RecentlyViewed { get; set; }

        [JsonProperty("favorites")]
        public ObservableCollection<string> Favorites { get; set; }

        [JsonProperty("recent_comments")]
        public ObservableCollection<string> RecentComments { get; set; }
    }
}