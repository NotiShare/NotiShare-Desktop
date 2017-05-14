using Newtonsoft.Json;

namespace Notishare.Model.DataTypes
{
    public class RegisterDeviceObject
    {
        
        [JsonProperty(PropertyName = "userName")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty(PropertyName = "deviceType")]
        public string DeviceType { get; set; }
    }
}
