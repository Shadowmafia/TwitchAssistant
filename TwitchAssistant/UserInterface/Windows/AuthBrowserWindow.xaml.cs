using System.Windows;
using AssistantConfig;
using Tools;

namespace TwitchAssistant.UserInterface.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthBrowserView.xaml
    /// </summary>
    public partial class AuthBrowserView : Window
    {
    
        public AuthBrowserView()
        {
           
            InitializeComponent();
        }
      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Отправляем пользователя на страницу авторизации на твиче, для получению доступа к его учетной записи и токена авторизации на на HttpServer
            string authReqestUrl = $"https://id.twitch.tv/oauth2/authorize?response_type=code&client_id={ AppTwitchTokens.TwitchClientId  }&redirect_uri=http://localhost:{ConfigSet.Config.Port}/twitch/callback&scope="+
            "user_read+user_blocks_edit+user_blocks_read+user_follows_edit+channel_read+channel_editor+channel_commercial+channel_stream+channel_subscriptions+user_subscriptions+channel_check_subscription+chat_login+channel_feed_read+channel_feed_edit+collections_edit+communities_edit+communities_moderate+viewing_activity_read+analytics:read:extensions+user:edit+user:read:email+clips:edit+bits:read+analytics:read:games+user:edit:broadcast+user:read:broadcast+chat:read+chat:edit+channel:moderate+whispers:read+whispers:edit"+
            "&force_verify=true";
            ConnectBrowser.MenuHandler = new MyCustomMenuHandler();
            ConnectBrowser.Address = authReqestUrl;         
        }

    }
}
