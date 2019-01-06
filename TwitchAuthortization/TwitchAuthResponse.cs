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
        
        public string access_token { get; set; }
        public string refresh_token { get; set; }     
        public List<string> scope { get; set; }
        public string code { get; set; }
    }
}
