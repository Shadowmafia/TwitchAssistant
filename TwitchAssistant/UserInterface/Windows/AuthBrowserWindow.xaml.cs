using System;
using System.Windows;
using AssistantConfig;
using CefSharp;
using CefSharp.Wpf;
using Tools;

namespace TwitchAssistant.UserInterface.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthBrowserView.xaml
    /// </summary>
    public partial class AuthBrowserWindow : Window
    {
        private ChromiumWebBrowser ConnectBrowser = new ChromiumWebBrowser();
        public AuthBrowserWindow(bool IsStreamerAuth)
        {
            //Cef settings
            RequestContextSettings requestContextSettings = new RequestContextSettings();
            requestContextSettings.PersistSessionCookies = false;
            requestContextSettings.PersistUserPreferences = false;
            if (IsStreamerAuth)
            {
                requestContextSettings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TwitchAssistant\Streamer";
            }
            else
            {
                requestContextSettings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TwitchAssistant\Bot";
            }       
            ConnectBrowser = new ChromiumWebBrowser();
            ConnectBrowser.MenuHandler = new MyCustomMenuHandler();
            ConnectBrowser.RequestContext = new RequestContext(requestContextSettings);

            InitializeComponent();
            BrowserContainer.Children.Add(ConnectBrowser);
        }
      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {       
            //Отправляем пользователя на страницу авторизации на твиче, для получению доступа к его учетной записи и токена авторизации на на HttpServer
            string authReqestUrl = $"https://id.twitch.tv/oauth2/authorize?response_type=code&client_id={ AppInteractionConfig.TwitchClientId  }&redirect_uri={AppInteractionConfig.OAuthRedirectUrl}&scope="+
            "user_read+user_blocks_edit+user_blocks_read+user_follows_edit+channel_read+channel_editor+channel_commercial+channel_stream+channel_subscriptions+user_subscriptions+channel_check_subscription+chat_login+channel_feed_read+channel_feed_edit+collections_edit+communities_edit+communities_moderate+viewing_activity_read+analytics:read:extensions+user:edit+user:read:email+clips:edit+bits:read+analytics:read:games+user:edit:broadcast+user:read:broadcast+chat:read+chat:edit+channel:moderate+whispers:read+whispers:edit"+
            "&force_verify=true";       
            ConnectBrowser.Address = authReqestUrl;         
        }

    }
}
