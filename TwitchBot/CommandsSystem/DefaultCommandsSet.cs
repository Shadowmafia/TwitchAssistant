using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AssistantConfig;
using AssitantPlayer;
using DataClasses.Enums;
using DateBaseController;
using DateBaseController.Models;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem
{
    public static class DefaultCommandsSet
    {
        public static CommandsController Commands;

        static DefaultCommandsSet()
        {
          
            Commands = new CommandsController();
            //  InitDefaultCommands();
            InitUserCommands();
        }



        public static void SaveCommands()
        {
            List<DefaultCommand> userCommands = new List<DefaultCommand>();
            foreach (var command in Commands.CommandList)
            {
                userCommands.Add(new DefaultCommand() { Id = command.Id, Name = command.Name, Message = command.Message, IsEnabled = command.IsEnabled, Whisp = command.Whisp, Description = command.Description });
            }

            (AssistantDb.Instance.DefaultCommands.GetAll()).ToList().AddRange(userCommands);
            AssistantDb.Instance.SaveChanges();
        }
        private static void InitUserCommands()
        {
            Action<ChatMessage, string, BotCommand>[] comActions = new Action<ChatMessage, string, BotCommand>[]
            {
                sayHello, songRequest, songRequestFirst, skipSong, songQeueLeght, curentSong, streamerPlaylist,
                chatPlaylist, myInfo,
                userInfo, coins, upTime, help

            };
            List<DefaultCommand> userCommands = AssistantDb.Instance.DefaultCommands.GetAll().ToList();

            for (int i = 0; i < userCommands.Count; i++)
            {
                BotCommand hello = new BotCommand(userCommands[i].Name, userCommands[i].Id, comActions[userCommands[i].Id - 1])
                {
                    Description = userCommands[i].Description,
                    IsEnabled = userCommands[i].IsEnabled,
                    Message = userCommands[i].Message,
                    Whisp = userCommands[i].Whisp
                };


                Commands.AddCommand(hello);
            }

        }
        private static void InitDefaultCommands()
        {
            BotCommand hello = new BotCommand("hello", 1, sayHello);
            hello.Description = "Say hello with you name";

            Commands.AddCommand(hello);
            //Player commands
            BotCommand sr = new BotCommand("sr", 2, songRequest);
            sr.Description = "(and youtube link) Add song to chat playlist";

            BotCommand srf = new BotCommand("srf", 3, songRequestFirst);
            srf.Description = "(and youtube link) Add song to chat playlist with out queue";

            BotCommand ss = new BotCommand("ss", 4, skipSong);
            ss.Description = "Skip current song";

            BotCommand sql = new BotCommand("sql", 5, songQeueLeght);
            sql.Description = "Chat playlist queue length";

            BotCommand cs = new BotCommand("song", 6, curentSong);
            cs.Description = "Current song info";

            BotCommand spl = new BotCommand("playlist", 7, streamerPlaylist);
            spl.Description = "Get streamer playlist";

            BotCommand cpl = new BotCommand("cPlaylist", 8, chatPlaylist);
            cpl.Description = "Get chat playlist";

            Commands.AddCommand(sr);
            Commands.AddCommand(srf);
            Commands.AddCommand(ss);

            Commands.AddCommand(cs);
            Commands.AddCommand(sql);

            Commands.AddCommand(spl);
            Commands.AddCommand(cpl);

            //Db Commands 
            BotCommand mInfo = new BotCommand("myInfo", 9, myInfo);
            mInfo.Description = "Check you info";

            BotCommand uInfo = new BotCommand("userInfo", 10, userInfo);
            uInfo.Description = "(and username) Check user info";

            BotCommand ci = new BotCommand("coins", 11, coins);
            ci.Description = $"Check you {ConfigSet.Config.CoinConfig.CoinsName}";


            BotCommand upt = new BotCommand("uptime", 12, upTime);
            upt.Description = $"Get translation upTime";

            Commands.AddCommand(mInfo);
            Commands.AddCommand(uInfo);
            Commands.AddCommand(ci);
            Commands.AddCommand(upt);

            BotCommand hlp = new BotCommand("help", 13, help);
            hlp.Description = $"Get all enabled chat commands.";
            Commands.AddCommand(hlp);
        }

        //Player methods
        private static void songRequest(ChatMessage user, string body, BotCommand command)
        {
           
            string playerResponse = "empty";
            try
            {
                if (ConfigSet.Config.PlayerConfig.ChatPlaylistOn)
                {
                    var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == user.Username);
                    MinRangCheck(tmpUser);
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
            CommandMessage(user.Username, playerResponse, command);
        }

        private static void songRequestFirst(ChatMessage user, string body, BotCommand command)
        {
            string playerResponse = "empty";

            try
            {
                if (ConfigSet.Config.PlayerConfig.ChatPlaylistOn)
                {
                    var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == user.Username);
                    MinRangCheck(tmpUser);
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

            CommandMessage(user.Username, playerResponse, command);
        }
        private static void skipSong(ChatMessage user, string body, BotCommand command)
        {
            string responseMessage = "";
            bool coinSystemSuccess = false;

            try
            {
                if (ConfigSet.Config.PlayerConfig.IsUsingCoinSystem)
                {
                    var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == user.Username);
                    MinRangCheck(tmpUser);

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

            CommandMessage(user.Username, responseMessage, command);
        }

        private static void curentSong(ChatMessage user, string body, BotCommand command)
        {
            string responseMessage = MyPlayer.Instance.GetCurrentSongInfo();
            CommandMessage(user.Username, responseMessage, command);
        }
        private static void songQeueLeght(ChatMessage user, string body, BotCommand command)
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
            CommandMessage(user.Username, responseMessage, command);
        }

        private static void streamerPlaylist(ChatMessage user, string body, BotCommand command)
        {
            string responseMessage = MyPlayer.Instance.GetStreamerPlaylistInfo();
            if (responseMessage == "")
            {
                responseMessage = "Streamer playlist is empty";
            }
            CommandMessage(user.Username, responseMessage, command);
        }

        private static void chatPlaylist(ChatMessage user, string body, BotCommand command)
        {
            string responseMessage = MyPlayer.Instance.GetChatPlaylistInfo();
            if (responseMessage == "")
            {
                responseMessage = "Chat playlist is empty";
            }
            CommandMessage(user.Username, responseMessage, command);
        }

        //Db methods 
        private static void myInfo(ChatMessage user, string body, BotCommand command)
        {
            var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == user.Username);
            string responseMessage = $@"info by user : {tmpUser.Username} 
             ||| Coins :{tmpUser.Coins} 
             ||| Watching Time :{tmpUser.WatchingTime} 
             ||| IsFollower :{tmpUser.IsFollower} 
             ||| IsSubscriber :{tmpUser.IsSubscriber} 
             ||| First connect to Stream :{tmpUser.FirstConnectToStream} 
             ||| Last connect to Stream :{tmpUser.LastConnectToStream} 
             ||| Last coin update :{tmpUser.LastCoinUpdate} 
              ";

            CommandMessage(user.Username, responseMessage, command);
        }
        private static void userInfo(ChatMessage user, string body, BotCommand command)
        {
            string responseMessage;
            try
            {
                var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == body);
                responseMessage = $@"info by user : {tmpUser.Username} 
                 ||| {ConfigSet.Config.CoinConfig.CoinsName} :{tmpUser.Coins} 
                 ||| Watching Time :{tmpUser.WatchingTime} 
                 ||| IsFollower :{tmpUser.IsFollower} 
                 ||| IsSubscriber :{tmpUser.IsSubscriber} 
                 ||| First connect to Stream :{tmpUser.FirstConnectToStream} 
                 ||| Last connect to Stream :{tmpUser.LastConnectToStream} 
                 ||| Last coin update :{tmpUser.LastCoinUpdate} 
                  ";
            }
            catch (Exception e)
            {
                responseMessage = $"User {body} not found";
            }

            CommandMessage(user.Username, responseMessage, command);
        }
        private static void coins(ChatMessage user, string body, BotCommand command)
        {
            var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == user.Username);
            string responseMessage = $"You balance : {tmpUser.Coins}";
            CommandMessage(user.Username, responseMessage, command);
        }

        private static void upTime(ChatMessage user, string body, BotCommand command)
        {
            string responseMessage = "";
            if (TwitchBotGlobalObjects.IsStreamOnline)
            {
                responseMessage = $"Stream up time :  {TwitchBotGlobalObjects.StreamUpTime} .";
            }
            else
            {
                responseMessage = "Stream offline !";
            }
            CommandMessage(user.Username, responseMessage, command);
        }
        //Other methods
        private static void help(ChatMessage user, string body, BotCommand command)
        {
            string result = "";
            int i = 1;
            foreach (var item in Commands.CommandList)
            {
                if (i >= Commands.CommandList.Count)
                {
                    continue;
                }
                if (item.IsEnabled)
                {
                    if ((result + $" {i}) !{item.Name} - {item.Description} ||").Length >= 350)
                    {
                        CommandMessage(user.Username, result, command);
                        Thread.Sleep(500);
                        result = "";
                    }
                    result += $" {i}) !{item.Name} - {item.Description} ||";
                    i++;
                }
            }
            CommandMessage(user.Username, result, command);
        }
        private static void sayHello(ChatMessage user, string body, BotCommand command)
        {
            string x = "Hello " + user.Username;
            CommandMessage(user.Username, x, command);
        }


        private static void CommandMessage(string user, string message, BotCommand command)
        {
            if (command.Message)
            {
                TwitchBotGlobalObjects.Bot.SendMessage($"{user} {message}");            
                Thread.Sleep(500);
            }
            if (command.Whisp)
            {
                TwitchBotGlobalObjects.Bot.WhispMessage(user, message);
            }
        }

        private static void MinRangCheck(Viewer user)
        {
            if (ConfigSet.Config.PlayerConfig.MinRequestRang == TwitchRangs.Follower)
            {
                if (!user.IsFollower)
                {
                    throw new Exception($"You must be '{ConfigSet.Config.PlayerConfig.MinRequestRang}' or upper");
                }
            }
            if (ConfigSet.Config.PlayerConfig.MinRequestRang == TwitchRangs.Subscriber)
            {
                if (!user.IsSubscriber)
                {
                    throw new Exception($"You must be '{ConfigSet.Config.PlayerConfig.MinRequestRang}' or upper");
                }
            }
        }

        
    }
}
