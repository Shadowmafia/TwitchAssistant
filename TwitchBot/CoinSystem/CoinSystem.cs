using System;
using System.Timers;
using AssistantConfig;
using DateBaseController;
using DateBaseController.Models;
using Tools;

namespace TwitchBot.CoinSystem
{

    public class CoinSystem : SingletonBaseTemplate<CoinSystem>
    {
      
        public bool IsStarted { get; private set; }
        private readonly Timer _timer;
        
        private CoinSystem()
        {
            IsStarted = false;
            _timer = new Timer();
            _timer.Elapsed += AccrualCoins;
            _setInterval();
        }
        private void _setInterval()
        {
            _timer.Interval = _GetInterval() * 10 * 1000;
        }
        private double _GetInterval()
        {
            if (TwitchBotGlobalObjects.IsStreamOnline)
            {
                return ConfigSet.Config.CoinConfig.StreamOnlineAccrualInterval;
            }
            else
            {
                return ConfigSet.Config.CoinConfig.StreamOfflineAccrualInterval;
            }
        }
        private void AccrualCoins(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            if (TwitchBotGlobalObjects.ChanelData.Watchers.Count > 0)
            {
                //TODO debug: AccuralCount.

                foreach (var watcher in TwitchBotGlobalObjects.ChanelData.Watchers)
                {
                    int accrualCounts;
                    watcher.WatchingTime = watcher.WatchingTime.Add(DateTime.Now - watcher.LastConnectToStream);
                    watcher.LastConnectToStream = DateTime.Now;

                    if (watcher.IsFollower)
                    {
                        accrualCounts = ConfigSet.Config.CoinConfig.Follower;
                    }
                    else
                    {
                        accrualCounts = ConfigSet.Config.CoinConfig.Unfollow;
                    }

                    if (accrualCounts == 0)
                    {
                        continue;
                    }
                    TimeSpan watchTime = DateTime.Now - watcher.LastCoinUpdate;
                    if (watchTime.TotalMinutes >= _GetInterval())
                    {
                        decimal s = (decimal)(_GetInterval() * 60) / accrualCounts;
                        decimal surplusMinutes = (decimal)watchTime.TotalSeconds % s;

                        //Todo: TotalMinutes its double value, maybe change later.
                        int sum = (int)(watchTime.TotalMinutes * ((int)accrualCounts / _GetInterval()));
                        AddCoins(watcher, sum);
                        watcher.LastCoinUpdate = DateTime.Now.Subtract(new TimeSpan(0, 0, (int)surplusMinutes));
                    }

                }
                AssistantDb.Instance.SaveChanges();
            }
            _timer.Start();
        }

        public void Start()
        {
            if (!IsStarted && ConfigSet.Config.CoinConfig.IsEnabled)
            {

                try
                {
                    _setInterval();
                    _timer.Start();
                    IsStarted = true;
                }
                catch (Exception e)
                {
                    IsStarted = false;
                }
            }
        }
        public void Stop()
        {
            _timer.Start();
        }
   
        public void AddCoins(Viewer user, int coins)
        {
            user.Coins += coins;
        }
        public bool SubtractCoins(Viewer user, int coins)
        {
            if (ConfigSet.Config.CoinConfig.IsEnabled)
            {
                if (user.Coins - coins < 0)
                {
                    return false;
                }
                else
                {
                    user.Coins -= coins;
                }
            }
            return true;
        }

    
    }

}
