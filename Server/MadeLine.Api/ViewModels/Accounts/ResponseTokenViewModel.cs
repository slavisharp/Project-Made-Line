namespace MadeLine.Api.ViewModels.Accounts
{
    using Newtonsoft.Json;

    public class ResponseTokenViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("auth_token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public int Expires { get; set; }
    }
}
