using Newtonsoft.Json;

namespace Mojang.NET.Profile
{
    public struct NameHistory
    {
        [JsonProperty("name")] 
        public string Name { get; set; }

        [JsonProperty("changedToAt")] 
        public long? ChangedToAt { get; set; }

        public bool IsCurrentUsername()
        {
            return ChangedToAt == null;
        }
    }
}