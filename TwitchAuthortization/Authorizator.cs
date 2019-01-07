using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools;


namespace TwitchAuthortization
{
    public class Authorizator
    {

        private readonly string _twitchClientId;
        private readonly string _twitchClientSecret;
        private readonly string _twitchRedirectUri;
        private Action<TwitchAuthResponse> _transferTokensCallBack;

        /// <summary>
        /// Redirect uri must be with out "/" in end .
        /// </summary>
        public Authorizator(string ClientId, string ClientSecret, string RedirectUri)
        {
            _twitchClientId = ClientId;
            _twitchClientSecret = ClientSecret;
            _twitchRedirectUri = RedirectUri;
        }

        /// <summary>
        /// This method starting http server and wait when user give access to him channel, afer method send reqest and return object with twitch tokens.
        /// (return null if authorization error).
        /// </summary>
        /// <param name="callBack">TwitchAuthResponse this object with all twitch token</param>
        public void InitHttpServerAndWaitCallBack(Action<TwitchAuthResponse> callBack)
        {           
            Task.Run(() =>
            {
                try
                {
                    _transferTokensCallBack = callBack;
                    HttpServer.InitHttpServer(this.TwitchAuthorizationApi, _twitchRedirectUri);
                    HttpServer.Run();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            );
        }

        /// <summary>
        /// You can acces tokens with out http server if you have twtich code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<TwitchAuthResponse> TwitchAuthorizationApi(string code)
        {

            TwitchAuthResponse myAuthResponse = null;
            NameValueCollection postDataDictionary = new NameValueCollection();

            postDataDictionary.Add("client_id", _twitchClientId);
            postDataDictionary.Add("client_secret", _twitchClientSecret);
            postDataDictionary.Add("grant_type", "authorization_code");
            postDataDictionary.Add("redirect_uri", _twitchRedirectUri);
            postDataDictionary.Add("state", "123456");
            postDataDictionary.Add("code", code);

            try
            {
                myAuthResponse = JsonConvert.DeserializeObject<TwitchAuthResponse>(await GetPostWeb.RequestAsync("https://api.twitch.tv/kraken/oauth2/token", "POST", postDataDictionary));
                myAuthResponse.Code = code;
            }
            catch 
            {
               // throw ex;
            }
            _transferTokensCallBack(myAuthResponse);
            return myAuthResponse;
        }
    }
}