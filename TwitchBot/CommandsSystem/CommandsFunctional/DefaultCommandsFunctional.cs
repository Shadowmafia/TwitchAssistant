using AssistantConfig;
using DateBaseController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using DateBaseController.Models;
using TwitchBot.CommandsSystem.Commands;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.CommandsFunctional
{
   public static class DefaultCommandsFunctional
    {
        public static Dictionary<string, Action<ChatMessage, string, DefaultBotCommand>> Actions = new Dictionary<string, Action<ChatMessage, string, DefaultBotCommand>>();
        static DefaultCommandsFunctional()
        {
            Actions.Add("MyInfo", MyInfo);
            Actions.Add("UserInfo", UserInfo);
            Actions.Add("Coins", Coins);
            Actions.Add("UpTime", UpTime);
            Actions.Add("SayHello", SayHello);
            Actions.Add("Help", Help);
        }


        //Db methods
        public static void MyInfo(ChatMessage user, string body, DefaultBotCommand command)
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

            Bot.CommandMessage(user.Username, responseMessage, command);
        }
        public static void UserInfo(ChatMessage user, string body, DefaultBotCommand command)
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

            Bot.CommandMessage(user.Username, responseMessage, command);
        }
        public static void Coins(ChatMessage user, string body, DefaultBotCommand command)
        {
            var tmpUser = AssistantDb.Instance.Viewers.GetAll().First(user1 => user1.Username == user.Username);
            string responseMessage = $"You balance : {tmpUser.Coins}";
            Bot.CommandMessage(user.Username, responseMessage, command);
        }
        //Other methods
        public static void UpTime(ChatMessage user, string body, DefaultBotCommand command)
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
            Bot.CommandMessage(user.Username, responseMessage, command);
        }               
        public static void SayHello(ChatMessage user, string body, DefaultBotCommand command)
        {
            string x = "Hello " + user.Username;
            Bot.CommandMessage(user.Username, x, command);
        }

        private static void Help(ChatMessage user, string body, DefaultBotCommand command)
        {

            int currentIndex = PrintCommandList(CommandsController.DefaultCommandsList, 0, user, command)-1;
            PrintCommandList(CommandsController.PlayerCommandsList,currentIndex, user, command);
        }
        private static int PrintCommandList<T>(ObservableCollection<T> commandList,int startIndex, ChatMessage user, DefaultBotCommand command) where T:BotCommand<T>
        {
            string result = "";
            int i = 1;
            foreach (var item in commandList)
            {
                if (item.Id==1 && item is DefaultBotCommand)
                {
                   continue;
                }
                if (item.IsEnabled)
                {
                    if ((result + $" {i+ startIndex}) !{item.Name} - {item.Description} ||").Length >= 350)
                    {
                        Bot.CommandMessage(user.Username, result, command);
                        Thread.Sleep(200);
                        result = "";
                    }
                    result += $" {i + startIndex}) !{item.Name} - {item.Description} ||";
                    i++;
                }
            }
            Bot.CommandMessage(user.Username, result, command);
            return i;
        }
    }
}
