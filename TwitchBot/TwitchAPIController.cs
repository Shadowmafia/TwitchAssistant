using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using AssistantConfig;
using Tools;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Games;
using TwitchLib.Api.V5.Models.Channels;

namespace TwitchBot
{
    public class TwitchApiNotAvailable : Exception
    {
        public TwitchApiNotAvailable()
        {

        }
        public TwitchApiNotAvailable(string message) : base(message)
        {

        }
    }

    public  static class TwitchApiController
    {     
        public static TwitchAPI Api{get;set;}
        public static ChannelAuthed Channel;

        public static void AuthApiController(string clientId)
        {          
            Api = new TwitchAPI();
            Api.Settings.ClientId = clientId;
            Api.Settings.AccessToken = ConfigSet.Config.Auth.StreamerAuth.Tokens.AccessToken;
            Channel = GetChanel().Result;
            ConfigSet.Config.BotConfig.StreamName = Channel.DisplayName;       
        }
        
        public static async Task<ChannelAuthed> GetChanel()
        {
            ChannelAuthed chanel = await Api.V5.Channels.GetChannelAsync(ConfigSet.Config.Auth.StreamerAuth.Tokens.AccessToken);
            return chanel;
        }

        public static async Task<TwitchLib.Api.V5.Models.Users.User> GetUserByName(string name)
        {
            TwitchLib.Api.V5.Models.Users.Users x = await Api.V5.Users.GetUserByNameAsync(name);
            try
            {
                return x.Matches[0];
            }
            catch (Exception e)
            {
                Logger.Instance.WriteLog($"Api controller GetUserByName ex : Err User not found . Details : {e.Message}", "TwitchApiController_GetUserByName");
               
            }
            return null;
        }
       
        public static async Task<List<TwitchLib.Api.Core.Models.Undocumented.Chatters.ChatterFormatted>> GetChatters()
        {        
            List<TwitchLib.Api.Core.Models.Undocumented.Chatters.ChatterFormatted> x = await Api.Undocumented.GetChattersAsync(Channel.Name);
            return x;
        }
        public static async Task<List<TwitchLib.Api.V5.Models.Channels.ChannelFollow>> GetFollowes()
        {
            var x = await Api.V5.Channels.GetAllFollowersAsync(Channel.Id);
            return x;
        }
        public static async Task<TwitchLib.Api.V5.Models.Channels.ChannelSubscribers> GetSubscribers()
        {           
            var x = await Api.V5.Channels.GetChannelSubscribersAsync(Channel.Id);
            return x;
        }
      
      
        public static async Task<Game[]> GetAllGames()
        {
            GetTopGamesResponse games = await Api.Helix.Games.GetTopGamesAsync(null, null, 100);
            return games.Data;
        }

        /// <summary>
        /// Throw TwitchApiNotAvailable if cant update chanel
        /// </summary>
        /// <param name="Title">New channel title </param>
        /// <param name="Game">New channel game</param>
        public static async void UpdateGameAndTitle(string Title,string Game)
        {
            try
            {
                await Api.V5.Channels.UpdateChannelAsync(Channel.Id, Title, Game);
            }
            catch (Exception e)
            {
                throw new TwitchApiNotAvailable("Twitch api not available! try later");
            }     
        }

        /// <summary>
        /// Return stream up time or null if stream offline
        /// </summary>
        public static async Task<TimeSpan?> GetStreamUpTime()
        {         
            var online = await CheckStreamOnline();
            if (!online)
            {             
                return null;
            }
            TimeSpan? upTime = await Api.V5.Streams.GetUptimeAsync(Channel.Id);
            return upTime ?? null;
        }
        public static async Task<bool> CheckStreamOnline()
        {
           return await Api.V5.Streams.BroadcasterOnlineAsync(Channel.Id);
        }
        public static string CheckAccessToken(string accessToken)
        {
            NameValueCollection headers = new NameValueCollection();
            headers.Add("Authorization", $"OAuth {accessToken}");
            string response = GetPostWeb.Request("https://id.twitch.tv/oauth2/validate", "GET", "", null,
                headers);
            return response;
        }
    }
 
}
