using System.Windows.Media;
using AssistantConfig.ConfigEntities;
using DataClasses.ConfigDataClasses;
using DataClasses.Enums;
using TwitchAuthortization;
using TwitchLib.Client.Enums;

namespace AssistantConfig
{
    public class Config
    {

        public string Port { set; get; }
        public AuthorizationConfig Auth { set; get; }
        public BotConfig BotConfig { set; get; }
        public CoinSystemConfig CoinConfig { set; get; }
        public MiniChatConfig MiniChatConfig { set; get; }
        public PlayerConfig PlayerConfig { set; get; }

        public Config()
        {
            Port = "";
            Auth = new AuthorizationConfig();
            BotConfig = new BotConfig();
            CoinConfig = new CoinSystemConfig();
            MiniChatConfig = new MiniChatConfig();
            PlayerConfig = new PlayerConfig();
        }

        /// <summary>
        /// Set all application config to default
        /// </summary>
        /// <param name="debugMode">if true then debug mode</param>      
        public void DefaultConfig(bool debugMode = true)
        {

            //personal data
            if (debugMode)
            {
                DebugDataConfig();
            }
            else
            {
                BotConfig = new BotConfig()
                {
                    IsDualMode = false,
                    BotName = "YouBotName",
                    StreamName = "YouStreamName",
                    BotColor = new BotColor() { RbgColor = new System.Windows.Media.SolidColorBrush(Color.FromRgb(255, 0, 0)), Color = ChatColorPresets.Red }
                };
                Auth = new AuthorizationConfig()
                {
                    BotAuth = new AuthorizationInstance()
                    {
                        Tokens = new TwitchAuthResponse()
                        {
                            AccessToken = "You need get oauth token"
                        }
                    },
                    StreamerAuth = new AuthorizationInstance()
                    {
                        Tokens = new TwitchAuthResponse()
                        {
                            AccessToken = "You need get oauth token"
                        }
                    }
                };
                CoinConfig = new CoinSystemConfig()
                {
                    StreamOfflineAccrualInterval = 1,
                    StreamOnlineAccrualInterval = 1,
                    IsEnabled = true,
                    CoinsName = "Coins",
                    CoinSystemCustomMode = false,
                    Unfollow = 5,
                    Follower = 10,
                    Subscriber = 20,
                };
            }
           

            Port = "8080";      
            MiniChatConfig.ChatUsage = true;
            MiniChatConfig.Topmost = true;
            MiniChatConfig.IsLocked = false;
            MiniChatConfig.ChatSettingsAndInput = true;
            MiniChatConfig.ChatPosition = new Point(100, 200);
            MiniChatConfig.ChatSize = new Size(400, 300);
            MiniChatConfig.BgColor = Color.FromArgb(255, 239, 238, 241);


            PlayerConfig.Autoplay = true;
            PlayerConfig.ChatPlaylistOn = true;

            PlayerConfig.IsUsingCoinSystem = true;
            PlayerConfig.CurrentSongNotify = true;

            PlayerConfig.SongPrice = 10;
            PlayerConfig.SkipSongPrice = 30;
            PlayerConfig.FirstSongPrice = 50;

            PlayerConfig.UnfollowRequestsPerHours = 3;
            PlayerConfig.FollowerRequestsPerHours = 6;
            PlayerConfig.SubscriberRequestsPerHours = 10;

            PlayerConfig.MinRequestRang = TwitchRangs.Unfollower;

        }

        private void DebugDataConfig()
        {
            BotConfig = new BotConfig()
            {
                IsDualMode = false,
                BotName = "Valerabot",
                StreamName = "imshadowmafia",
                BotColor = new BotColor() { RbgColor = new System.Windows.Media.SolidColorBrush(Color.FromRgb(255, 0, 0)), Color = ChatColorPresets.Red }
            };
            Auth = new AuthorizationConfig()
            {
                BotAuth = new AuthorizationInstance()
                {
                    Tokens = new TwitchAuthResponse()
                    {
                        AccessToken = "51o0g399yiaqsab1vde59kpba4f685"
                    }
                },
                StreamerAuth = new AuthorizationInstance()
                {
                    Tokens = new TwitchAuthResponse()
                    {
                        AccessToken = "51o0g399yiaqsab1vde59kpba4f685"
                    }
                }
            };
            CoinConfig = new CoinSystemConfig()
            {
                StreamOfflineAccrualInterval = 1,
                StreamOnlineAccrualInterval = 2,
                IsEnabled = true,
                CoinsName = "ShadowCoins",
                CoinSystemCustomMode = false,
                Unfollow = 10,
                Follower = 15,
                Subscriber = 20,
            };
        }
    }
}