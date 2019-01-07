using System.Windows;

namespace TwitchAssistant.Tests.AuthTests
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public string  BrowserUri { get; set; }
        public AuthWindow()
        {
            InitializeComponent();      
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WebBrowser.Address = BrowserUri;
        }
    }
}
