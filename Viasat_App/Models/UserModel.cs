namespace UserData
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
        public string[] PersonalNotesList {get; set;}

        [JsonProperty("history")]
        public string[] itemsHistory { get; set; }

        [JsonProperty("favorites")]
        public string[] favorites { get; set; }
    }

    public partial class userModel
    {
        public static List<UserModel> FromJson(string json) => JsonConvert.DeserializeObject<List<UserModel>>(json, UserData.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<UserModel> self) => JsonConvert.SerializeObject(self, UserData.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}