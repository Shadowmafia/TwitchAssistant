using System;
using System.ComponentModel;
using DataClasses;
using DataClasses.Enums;

namespace TwitchAssistant.TwitchBot
{
    public static class TwitchBotGlobalObjects
    {

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void OnStaticPropertyChanged(string propertyName)
        {
            var handler = StaticPropertyChanged;
            if (handler != null)
            {
                handler(null, new PropertyChangedEventArgs(propertyName));
            }
        }

        //public static MyPlayer Player { get; set; }
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


        static TwitchBotGlobalObjects()
        {
            ChanelData = new ChanelData();
            Bot = null;
            TwitchBotConnectedState = TwitchBotConnectedState.Disconnected;
            IsStreamOnline = false;
        }
    }
}
