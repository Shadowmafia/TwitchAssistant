using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TwitchAssistant.UserInterface.ViewModels;
using TwitchAssistant.UserInterface.Windows;


namespace TwitchAssistant
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RootWindow windowView = new RootWindow();
            windowView.DataContext = new RootWindowViewModel();
            windowView.Show();
        }
    }
    public static class AppInteractionConfig
    {
        public const string TwitchClientId = "st6xynr6cw85jt6qaz0muqjf4km1k7";
        public const string TwitchClientSecret = "8ljxrb0fx7koj8p6igs2l4cp62jhrf";
        public const string OAuthRedirectUrl = "http://localhost:8080/twitch/callback";
    }

}
