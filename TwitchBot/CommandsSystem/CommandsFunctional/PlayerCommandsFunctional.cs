using AssistantConfig;
using AssitantPlayer;
using DateBaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.CommandsSystem.Commands;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.CommandsFunctional
{
    public static class PlayerCommandsFunctional
    {
        public static Dictionary<string, Action<ChatMessage, string, PlayerBotCommand>> Actions = new Dictionary<string, Action<ChatMessage, string, PlayerBotCommand>>();
        static PlayerCommandsFunctional()
        {
            Actions.Add("SongRequest", SongRequest);
            Actions.Add("SongRequestFirst", SongRequestFirst);
            Actions.Add("SkipSong", SkipSong);
            Actions.Add("CurentSong", CurentSong);
            Actions.Add("SongQeueLeght", SongQeueLeght);
            Actions.Add("StreamerPlaylist", StreamerPlaylist);
            Actions.Add("ChatPlaylist", ChatPlaylist);
        }
        public static void SongRequest(ChatMessage user, string body, PlayerBotCommand command)
        {
            string playerResponse = "empty";
            try
            {
                if (ConfigSet.Config.PlayerConfig.ChatPlaylistOn)
                {
                    var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == user.Username);
                    Bot.MinRangCheck(tmpUser);   
                    bool coinSystemSuccess = false;

                    if (ConfigSet.Config.PlayerConfig.IsUsingCoinSystem)
                    {

                        if (CoinSystem.CoinSystem.Instance.SubtractCoins(tmpUser, ConfigSet.Config.PlayerConfig.SongPrice))
                        {
                            coinSystemSuccess = true;
                        }
                        else
                        {
                            playerResponse = $"not enough coins! Song price : {ConfigSet.Config.PlayerConfig.SongPrice} . You balance : {tmpUser.Coins} {ConfigSet.Config.CoinConfig.CoinsName}";
                        }
                    }
                    else
                    {
                        coinSystemSuccess = true;
                    }
                    if (coinSystemSuccess)
                    {
                        playerResponse = MyPlayer.Instance.AddChatSong(body, user.Username);
                        if (playerResponse == "")
                        {
                            playerResponse = "Song not found or invalid link =(";
                            if (ConfigSet.Config.PlayerConfig.IsUsingCoinSystem)
                            {
                                CoinSystem.CoinSystem.Instance.AddCoins(tmpUser, ConfigSet.Config.PlayerConfig.SongPrice);
                            }
                        }
                    }
                }
                else
                {
                    playerResponse = "Chat playlist disabled !";
                }
            }
            catch (Exception e)
            {
                playerResponse = e.Message;
            }
            Bot.CommandMessage(user.Username, playerResponse, command);
        }
        private static void SongRequestFirst(ChatMessage user, string body, PlayerBotCommand command)
        {
            string playerResponse = "empty";

            try
            {
                if (ConfigSet.Config.PlayerConfig.ChatPlaylistOn)
                {
                    var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == user.Username);
                    Bot.MinRangCheck(tmpUser);
                    bool coinSystemSuccess = false;

                    if (ConfigSet.Config.PlayerConfig.IsUsingCoinSystem)
                    {
                        if (CoinSystem.CoinSystem.Instance.SubtractCoins(tmpUser, ConfigSet.Config.PlayerConfig.FirstSongPrice))
                        {
                            coinSystemSuccess = true;
                        }
                        else
                        {
                            playerResponse = $"not enough coins! First song price : {ConfigSet.Config.PlayerConfig.FirstSongPrice} . You balance : {tmpUser.Coins} {ConfigSet.Config.CoinConfig.CoinsName}";
                        }
                    }
                    else
                    {
                        coinSystemSuccess = true;
                    }

                    if (coinSystemSuccess)
                    {
                        playerResponse = MyPlayer.Instance.AddFirstChatSong(body, user.Username);
                        if (playerResponse == "")
                        {
                            playerResponse = "Song not found or invalid link =(";
                            if (ConfigSet.Config.PlayerConfig.IsUsingCoinSystem)
                            {
                                CoinSystem.CoinSystem.Instance.AddCoins(tmpUser, ConfigSet.Config.PlayerConfig.SongPrice);
                            }
                        }
                    }
                }
                else
                {
                    playerResponse = "Chat playlist disabled !";
                }
            }
            catch (Exception e)
            {
                playerResponse = e.Message;
            }

            Bot.CommandMessage(user.Username, playerResponse, command);
        }
        private static void SkipSong(ChatMessage user, string body, PlayerBotCommand command)
        {
            string responseMessage = "";
            bool coinSystemSuccess = false;

            try
            {
                if (ConfigSet.Config.PlayerConfig.IsUsingCoinSystem)
                {
                    var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == user.Username);
                    Bot.MinRangCheck(tmpUser);

                    if (CoinSystem.CoinSystem.Instance.SubtractCoins(tmpUser, ConfigSet.Config.PlayerConfig.SkipSongPrice))
                    {
                        coinSystemSuccess = true;
                    }
                    else
                    {
                        responseMessage = $"not enough coins! Skip song price : {ConfigSet.Config.PlayerConfig.SkipSongPrice} . You balance : {tmpUser.Coins} {ConfigSet.Config.CoinConfig.CoinsName}";
                    }
                }
                if (coinSystemSuccess)
                {
                    responseMessage = $"skipped song! Title : '{MyPlayer.Instance.CurrentSong.Title}'";
                    MyPlayer.Instance.PlayNextSong();
                }
            }
            catch (Exception e)
            {
                responseMessage = e.Message;
            }

            Bot.CommandMessage(user.Username, responseMessage, command);
        }
        public static void CurentSong(ChatMessage user, string body, PlayerBotCommand command)
        {
            string responseMessage = MyPlayer.Instance.GetCurrentSongInfo();
            Bot.CommandMessage(user.Username, responseMessage, command);
        }
        public static void SongQeueLeght(ChatMessage user, string body, PlayerBotCommand command)
        {
            string responseMessage = "";
            try
            {
                var length = MyPlayer.Instance.ChatPlayList.Count - MyPlayer.Instance.LastChatSong.Index;
                responseMessage = $"In playlist queue {length} song(s)";
            }
            catch (Exception e)
            {
                responseMessage = $"Playlist is empty!";
            }
            Bot.CommandMessage(user.Username, responseMessage, command);
        }
        public static void StreamerPlaylist(ChatMessage user, string body, PlayerBotCommand command)
        {
            string responseMessage = MyPlayer.Instance.GetStreamerPlaylistInfo();
            if (responseMessage == "")
            {
                responseMessage = "Streamer playlist is empty";
            }
            Bot.CommandMessage(user.Username, responseMessage, command);
        }
        public static void ChatPlaylist(ChatMessage user, string body, PlayerBotCommand command)
        {
            string responseMessage = MyPlayer.Instance.GetChatPlaylistInfo();
            if (responseMessage == "")
            {
                responseMessage = "Chat playlist is empty";
            }
            Bot.CommandMessage(user.Username, responseMessage, command);
        }

    }
}
