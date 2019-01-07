using System.Windows;
using Tools;
using TwitchAuthortization;

namespace TwitchAssistant.Tests.AuthTests
{
    public  class AuthorizatorTester : SingletonBaseTemplate<AuthorizatorTester>,ITest
    {
        private AuthorizatorTester(){}

        public bool Test()
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.BrowserUri = this.GetAuthUri();
            authWindow.Show();
            bool result;
            try
            {
                Authorizator auth = new Authorizator("st6xynr6cw85jt6qaz0muqjf4km1k7", "8ljxrb0fx7koj8p6igs2l4cp62jhrf", "http://localhost:8080/twitch/callback");
                auth.InitHttpServerAndWaitCallBack((TwitchAuthResponse) =>
                {
                    string responce = "AccessToken :" + TwitchAuthResponse.AccessToken + "\n RefreshToken:" + TwitchAuthResponse.RefreshToken + "\n RefreshToken:" + TwitchAuthResponse.Code;
                    MessageBox.Show(responce);
                });
            }
            catch
            {
                result = false;
            }
            result = true;
            return result;
        }

        public string GetAuthUri()
        {
            return $"https://id.twitch.tv/oauth2/authorize?response_type=code&client_id=st6xynr6cw85jt6qaz0muqjf4km1k7&redirect_uri=http://localhost:8080/twitch/callback&scope=" +
            "user_read+user_blocks_edit+user_blocks_read+user_follows_edit+channel_read+channel_editor+channel_commercial+channel_stream+channel_subscriptions+user_subscriptions+channel_check_subscription+chat_login+channel_feed_read+channel_feed_edit+collections_edit+communities_edit+communities_moderate+viewing_activity_read+analytics:read:extensions+user:edit+user:read:email+clips:edit+bits:read+analytics:read:games+user:edit:broadcast+user:read:broadcast+chat:read+chat:edit+channel:moderate+whispers:read+whispers:edit" +
            "&force_verify=true";
        }
    }
}