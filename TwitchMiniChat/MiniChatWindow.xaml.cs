using System;
using System.Windows;
using AssistantConfig;
using CefSharp;
using CefSharp.Wpf;
using Tools;

namespace TwitchMiniChat
{
    /// <summary>
    /// Логика взаимодействия для MiniChatView.xaml
    /// </summary>
    public partial class MiniChatWindow : Window
    {
        public ChromiumWebBrowser ChatBrowser { get; set; }
        public MiniChatWindow()
        {
            //cef settings
            RequestContextSettings requestContextSettings = new RequestContextSettings();
            requestContextSettings.PersistSessionCookies = false;
            requestContextSettings.PersistUserPreferences = false;
            requestContextSettings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\TwitchAssistant\Streamer";
            ChatBrowser = new ChromiumWebBrowser();
            ChatBrowser.RequestContext = new RequestContext(requestContextSettings);
            ChatBrowser.MenuHandler = new MyCustomMenuHandler();
            ChatBrowser.BrowserSettings.BackgroundColor = 0x00;
            ChatBrowser.Name = "ChatBrowser";
           
            this.DataContext = new MiniChatViewModel(this);
            InitializeComponent();
            BrowserContainer.Children.Add(ChatBrowser);
            ChatBrowser.Address = $"https://www.twitch.tv/popout/{ ConfigSet.Config.BotConfig.StreamName }/chat?popout=";
           
        }

    }
}
