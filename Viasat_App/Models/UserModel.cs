namespace Viasat_App
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class UserModel
    {
        [JsonProperty("user_name")]
        public string UserName { get; set; }

        //[JsonProperty("user_last")]
        //public string UserLast { get; set; }

        [JsonProperty("permission_level")]
        public string PermissionLevel { get; set; }

        [JsonProperty("notes")]
        public int[] PersonalNotesList {get; set;}

        [JsonProperty("history")]
        public string[] itemsHistory { get; set; }

        [JsonProperty("favorites")]

    }
}