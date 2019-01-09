using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AssistantConfig;
using DataClasses.Enums;
using Tools;
using Tools.MVVMBaseClasses;

namespace AssitantPlayer.PlayerSettings
{

    public class RangItem
    {
        public TwitchRangs Rang { get; set; }

        public RangItem(TwitchRangs rang)
        {
            Rang = rang;
        }
    }

    public class PlayerSettingsViewModel : SingletonBaseTemplate<PlayerSettingsViewModel>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private PlayerSettingsViewModel() { }

        public static List<RangItem> RangsList { get; set; }

        public bool ChatPlaylistOn
        {
            get { return ConfigSet.Config.PlayerConfig.ChatPlaylistOn; }
            set
            {
                ConfigSet.Config.PlayerConfig.ChatPlaylistOn = value;
                OnPropertyChanged(nameof(ChatPlaylistOn));
            }
        }
        public bool UsingCoinSystem
        {
            get { return ConfigSet.Config.PlayerConfig.IsUsingCoinSystem; }
            set
            {
                ConfigSet.Config.PlayerConfig.IsUsingCoinSystem = value;
                OnPropertyChanged(nameof(UsingCoinSystem));
            }
        }
        public bool Autoplay
        {
            get { return ConfigSet.Config.PlayerConfig.Autoplay; }
            set
            {
                ConfigSet.Config.PlayerConfig.Autoplay = value;
                OnPropertyChanged(nameof(Autoplay));
            }
        }
        public bool CurrentSongNotify
        {
            get { return ConfigSet.Config.PlayerConfig.CurrentSongNotify; }
            set
            {
                ConfigSet.Config.PlayerConfig.CurrentSongNotify = value;
                OnPropertyChanged(nameof(CurrentSongNotify));
            }
        }

        public int SonPrice
        {
            get { return ConfigSet.Config.PlayerConfig.SongPrice; }
            set
            {
                ConfigSet.Config.PlayerConfig.SongPrice = value;
                OnPropertyChanged(nameof(SonPrice));
            }
        }
        public int FirstSongPrice
        {
            get { return ConfigSet.Config.PlayerConfig.FirstSongPrice; }
            set
            {
                ConfigSet.Config.PlayerConfig.FirstSongPrice = value;
                OnPropertyChanged(nameof(FirstSongPrice));
            }
        }
        public int SkipSongPrice
        {
            get { return ConfigSet.Config.PlayerConfig.SkipSongPrice; }
            set
            {
                ConfigSet.Config.PlayerConfig.SkipSongPrice = value;
                OnPropertyChanged(nameof(SkipSongPrice));
            }
        }

        public int UnfollowRequestsPerHours
        {
            get { return ConfigSet.Config.PlayerConfig.UnfollowRequestsPerHours; }
            set
            {
                ConfigSet.Config.PlayerConfig.UnfollowRequestsPerHours = value;
                OnPropertyChanged(nameof(UnfollowRequestsPerHours));
            }
        }
        public int FollowerRequestsPerHours
        {
            get { return ConfigSet.Config.PlayerConfig.FollowerRequestsPerHours; }
            set
            {
                ConfigSet.Config.PlayerConfig.FollowerRequestsPerHours = value;
                OnPropertyChanged(nameof(FollowerRequestsPerHours));
            }
        }
        public int SubscriberRequestsPerHours
        {
            get { return ConfigSet.Config.PlayerConfig.SubscriberRequestsPerHours; }
            set
            {
                ConfigSet.Config.PlayerConfig.SubscriberRequestsPerHours = value;
                OnPropertyChanged(nameof(SubscriberRequestsPerHours));
            }
        }

       
        public RangItem MinRequestRang
        {
            get { return RangsList.First(item => item.Rang == ConfigSet.Config.PlayerConfig.MinRequestRang); }
            set
            {
                ConfigSet.Config.PlayerConfig.MinRequestRang = value.Rang;
                OnPropertyChanged(nameof(MinRequestRang));
            }
        }

        static PlayerSettingsViewModel()
        {
            RangsList = new List<RangItem>
            {
                new RangItem(TwitchRangs.Unfollower),
                new RangItem(TwitchRangs.Follower),
                new RangItem(TwitchRangs.Subscriber)
            };

        }
    }
}
