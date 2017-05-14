using Newtonsoft.Json;

namespace Notishare.Model.DataTypes
{
    public class DeviceRegisterResult
    {
        [JsonProperty(PropertyName = "idUser")]
        public string UserDbId { get; set; }

        [JsonProperty(PropertyName = "idDevice")]
        public string DeviceDbId { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
