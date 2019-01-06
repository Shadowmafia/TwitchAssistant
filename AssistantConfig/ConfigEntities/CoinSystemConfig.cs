namespace AssistantConfig.ConfigEntities
{
    public class CoinSystemConfig
    {
        public bool IsEnabled { get; set; }
        public string CoinsName { get; set; }
        public bool CoinSystemCustomMode { get; set; }
        public int Unfollow { get; set; }
        public int Follower { get; set; }
        public int Subscriber { get; set; }
        public bool IsStatred { get; internal set; }

        public double StreamOnlineAccrualInterval { get; set; }
        public double StreamOfflineAccrualInterval { get; set; }
       

        public CoinSystemConfig()
        {
#if DEBUG
            //TODO: DebugInfo.
            IsEnabled = true;
            Unfollow = 0;
            Follower = 10;
            Subscriber = 20;

            StreamOnlineAccrualInterval = 1;
            StreamOfflineAccrualInterval = 1;

#endif
        }
    }
}
