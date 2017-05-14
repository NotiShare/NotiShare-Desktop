﻿using Newtonsoft.Json;

namespace Notishare.Model.DataTypes
{
    public class NotificationObject
    {

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "notificationText")]
        public string NotificationText { get; set; }


        [JsonProperty(PropertyName = "image")]
        public string ImageBase64 { get; set; }


    }
}