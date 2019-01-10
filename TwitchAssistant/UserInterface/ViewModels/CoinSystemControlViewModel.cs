using AssistantConfig;
using Tools.MVVMBaseClasses;

namespace TwitchAssistant.UserInterface.ViewModels
{
    class CoinSystemControlViewModel : ViewModelBase
    {
        public bool CoinSystemOn
        {
            get { return ConfigSet.Config.CoinConfig.IsEnabled; }
            set
            {
                ConfigSet.Config.CoinConfig.IsEnabled = value;
                OnPropertyChanged(nameof(CoinSystemOn));
            }
        }
        public string CoinsName
        {
            get { return ConfigSet.Config.CoinConfig.CoinsName; }
            set
            {
                ConfigSet.Config.CoinConfig.CoinsName = value;
                OnPropertyChanged(nameof(CoinsName));
            }
        }

        public int Unfollow
        {
            get { return ConfigSet.Config.CoinConfig.Unfollow; }
            set
            {
                ConfigSet.Config.CoinConfig.Unfollow = value;
                OnPropertyChanged(nameof(Unfollow));
            }
        }
        public int Follower
        {
            get { return ConfigSet.Config.CoinConfig.Follower; }
            set
            {
                ConfigSet.Config.CoinConfig.Follower = value;
                OnPropertyChanged(nameof(Follower));
            }
        }
        public int Subscriber
        {
            get { return ConfigSet.Config.CoinConfig.Subscriber; }
            set
            {
                ConfigSet.Config.CoinConfig.Subscriber = value;
                OnPropertyChanged(nameof(Subscriber));
            }
        }

        public double OnlineInterval
        {
            get { return ConfigSet.Config.CoinConfig.StreamOnlineAccrualInterval; }
            set
            {
                ConfigSet.Config.CoinConfig.StreamOnlineAccrualInterval = value;
                OnPropertyChanged(nameof(OnlineInterval));
            }
        }

        public double OfflineInterval
        {
            get { return ConfigSet.Config.CoinConfig.StreamOfflineAccrualInterval; }
            set
            {
                ConfigSet.Config.CoinConfig.StreamOfflineAccrualInterval = value;
                OnPropertyChanged(nameof(OfflineInterval));
            }
        }

    }
}
