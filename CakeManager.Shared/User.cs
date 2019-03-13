using Newtonsoft.Json;

namespace CakeManager.Shared
{
    public class User
    {
        public string UserName { get; set; }
        public UserProfile Profile { get; set; } = new UserProfile();
    }

    public class UserProfile
    {
        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }

        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }
    }
}
