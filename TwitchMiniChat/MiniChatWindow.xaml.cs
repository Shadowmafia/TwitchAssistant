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
        
        public MiniChatWindow()
        {
        
            this.DataContext = new MiniChatViewModel(this);
            InitializeComponent();
           // ChatBrowser.Source = $"https://www.twitch.tv/popout/{ ConfigSet.Config.BotConfig.StreamName }/chat?popout=";
            ChatBrowser.MenuHandler = new MyCustomMenuHandler();
            ChatBrowser.BrowserSettings.BackgroundColor = 0x00;
        }

      
    }
}
