using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UserType
{
    public class UserModel
    {
        [JsonProperty("_id")]
        public string _id { get; set; }

        [JsonProperty("user_name")]
        public string name { get; set; }

        [JsonProperty("user_last")]
        public string lastName { get; set; }

        [JsonProperty("permission_level")]
        public int permission_level { get; set; }

        [JsonProperty("notes")]
        public ObservableCollection<string> personal_notes {get; set;}

        [JsonProperty("recently_viewed")]
        public ObservableCollection<string> recently_viewed { get; set; }

        [JsonProperty("favorites")]
        public ObservableCollection<string> favorites { get; set; }

        //[JsonProperty("recent_comments")]
       //public ObservableCollection<string> RecentComments { get; set; }
    }
}