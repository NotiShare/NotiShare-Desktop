using Newtonsoft.Json;

namespace Notishare.Model.DataTypes
{
    public class RegistrationObject
    {
            
        [JsonProperty(PropertyName = "userName")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "passwordHash")]
        public string PasswordHash { get; set; }
    }
}
