using System.Threading.Tasks;
using System.Windows;
using AssistantConfig;
using TwitchAssistant.Tests.AuthTests;
using TwitchAssistant.TwitchBot;
using TwitchAuthortization;
using TwitchMiniChat;

namespace TwitchAssistant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // AuthorizatorTest.Instance.Test();
            // DateBaseTester.Instance.Test();
            //Authorizator auth = new Authorizator("st6xynr6cw85jt6qaz0muqjf4km1k7", "8ljxrb0fx7koj8p6igs2l4cp62jhrf", "http://localhost:8080/twitch/callback");
            //AuthWindow authWindow = new AuthWindow();
            //authWindow.BrowserUri = AuthorizatorTester.Instance.GetAuthUri();
            //authWindow.Show();

            //ConfigSet.Config.BotConfig.IsDualMode = false;
            //auth.InitHttpServerAndWaitCallBack((TwitchAuthResponse) =>
            //    {                
            //        ConfigSet.Config.Auth.StreamerAuth.Tokens = TwitchAuthResponse;
            //        ConfigSet.Config.Auth.BotAuth.Tokens = TwitchAuthResponse;

            //    });
            //ConfigSet.Config.BotConfig.IsDualMode = false;
            //GlobalObjects.Player.PlayerLoaded();
            ConfigSet.Config.BotConfig.IsDualMode = true;
            Task.Run(() =>
            {
                TwitchApiController.AuthApiController("st6xynr6cw85jt6qaz0muqjf4km1k7");
                TwitchBotGlobalObjects.Bot = new Bot();
                TwitchBotGlobalObjects.StreamUpDownChecker = new PubSub();
            });

           MiniChatWindow miniChatWindow = new MiniChatWindow();
           miniChatWindow.Show();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            //App.Current.Dispatcher.Invoke(() =>
            //{
            //    GlobalObjects.Player.PlayerClosed();
            //    ConfigSet.SaveConfig();
            //});
            
        }
    }
}
