using System.Windows;
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
            ChatBrowser.MenuHandler = new MyCustomMenuHandler();
            ChatBrowser.BrowserSettings.BackgroundColor = 0x00;
        }

      
    }
}
