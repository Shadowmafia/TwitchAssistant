using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DataClasses;
using DataClasses.Enums;
using DateBaseController;
using DateBaseController.Models;
using Tools;
using TwitchBot.CoinSystem;
using TwitchLib.Client.Models;

namespace TwitchBot
{
    public static class TwitchBotGlobalObjects
    {
    
        public static event PropertyChangedEventHandler StaticPropertyChanged;    
        private static void OnStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        static TwitchBotGlobalObjects()
        {
            ChanelData = new ChanelData();
            Bot = null;
            TwitchBotConnectedState = TwitchBotConnectedState.Disconnected;
            IsStreamOnline = false;
        }

        public static Bot Bot { get; set; }
        public static PubSub StreamUpDownChecker { get; set; }
        public static ChanelData ChanelData { get; set; }

        private static TwitchBotConnectedState _twitchBotConnectedState;
        public static TwitchBotConnectedState TwitchBotConnectedState
        {
            get => _twitchBotConnectedState;
            set
            {
                _twitchBotConnectedState = value;
                OnStaticPropertyChanged("TwitchBotConnectedState");
            }
        }

        private static bool _isStreamOnline;
        public static bool IsStreamOnline
        {
            get => _isStreamOnline;
            set
            {
                _isStreamOnline = value;
                OnStaticPropertyChanged("IsStreamOnline");
            }
        }
        private static TimeSpan? _streamUpTime;
        public static TimeSpan? StreamUpTime
        {
            get => _streamUpTime;
            set
            {
                _streamUpTime = value;
                OnStaticPropertyChanged("StreamUpTime");
            }
        }

        public static void CheckAndAddOrEditUserIfNeed(ChatMessage user)
        {

            Viewer joinedUser = AssistantDb.Instance.Viewers.GetAll().FirstOrDefault(viewer => viewer.Username == user.Username);
            if (joinedUser == null)
            {
                var result = TwitchApiController.Api.V5.Users.GetUserByNameAsync(user.Username).Result;
                int viewerId = int.Parse(result.Matches[0].Id);
                joinedUser = new Viewer() { Id = viewerId, Username = user.Username, Rang = new Rang() { RangSets = new List<RangSet>() { new RangSet() } } };
                joinedUser.IsModerator = user.IsModerator;
                joinedUser.IsSubscriber = user.IsSubscriber;
                joinedUser.IsBroadcaster = user.IsBroadcaster;
                var x = ChanelData.Followers.FirstOrDefault(tmpUser => tmpUser.User.Id == user.UserId);
                if (x != null)
                {
                    joinedUser.IsFollower = true;
                }
                AssistantDb.Instance.Viewers.Add(joinedUser);
                joinedUser = AssistantDb.Instance.Viewers.Get(joinedUser.Id);
                joinedUser.LastCoinUpdate = DateTime.Now;
                joinedUser.LastConnectToStream = DateTime.Now;
                AssistantDb.Instance.SaveChanges();
                ChanelData.Watchers.Add(joinedUser);
            }
            else
            {
                joinedUser.IsBroadcaster = user.IsBroadcaster;
                joinedUser.IsModerator = user.IsModerator;
                joinedUser.IsSubscriber = user.IsSubscriber;
                var x = ChanelData.Followers.FirstOrDefault(tmpUser => tmpUser.User.Id == user.UserId);
                if (x != null)
                {
                    joinedUser.IsFollower = true;
                }
                else
                {
                    joinedUser.IsFollower = false;
                }
            }
        }

    }
}
