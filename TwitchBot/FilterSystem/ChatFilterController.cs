using System;
using System.Collections.ObjectModel;
using DataClasses.Enums;
using TwitchBot.ChatFilter;
using TwitchBot.FilterSystem.MessageFilters;
using TwitchLib.Client.Models;

namespace TwitchBot.FilterSystem
{
    public static class ChatFilterController
    {
        public static ObservableCollection<BaseFilter> MessageFilters = new ObservableCollection<BaseFilter>();

        static ChatFilterController()
        {
            MessageFilters.Add(new LinksFilter()
            {
                IsEnabled = true,
                IsChatResponce = true,
                IsWhisResponce = false,
                IsTimeOut = true,
                IsUserName = true,
                TimeOutMessage = " Ну всё довыебывался",
                WarningMessage = " Не выебывайся а то получиш бан",
                MinIgnoreUserRang = TwitchRangs.Follower,
                UserTimeOutDuration = new TimeSpan(0,0,20),                
                CountingExecuteBeforeSanctions = 1,
                CountingExecuteBeforeTimeOut = 3,              
            });

            MessageFilters.Add(new CapsFilter()
            {
                IsEnabled = true,
                IsChatResponce = true,
                IsWhisResponce = false,
                IsTimeOut = true,
                IsUserName = true,
                TimeOutMessage = " Не капси сука!",
                WarningMessage = " Не выебывайся а то получиш бан",
                MinIgnoreUserRang = TwitchRangs.Follower,
                UserTimeOutDuration = new TimeSpan(0, 0, 3),
                CountingExecuteBeforeSanctions = 0,
                CountingExecuteBeforeTimeOut = 0,
            });

            MessageFilters.Add(new BadWordsFilter()
            {
                IsEnabled = true,
                IsChatResponce = true,
                IsWhisResponce = false,
                IsTimeOut = true,
                IsUserName = true,
                TimeOutMessage = " Не матерись гандон",
                WarningMessage = " Ток пиздани что то еще и я тебя выпилю",
                MinIgnoreUserRang = TwitchRangs.Follower,
                UserTimeOutDuration = new TimeSpan(0, 0, 15),
                CountingExecuteBeforeSanctions = 0,
                CountingExecuteBeforeTimeOut = 1,
            });
        }

        public static void FilterMessage(ChatMessage message)
        {
            foreach (BaseFilter filter in MessageFilters)
            {
                filter.ExecuteFilter(message);
            }
        }

    }
}
