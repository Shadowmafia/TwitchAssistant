using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchAuthortization
{
    //A class to represent the reponse from the Twitch auth call just to make it easy
    //to deserialize the oAuth access token
    public class TwitchAuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("scope")]
        public List<string> Scope { get; set; }
        public string Code { get; set; }
    }
}
