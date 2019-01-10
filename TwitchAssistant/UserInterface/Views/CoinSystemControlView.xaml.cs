using System.Windows.Controls;
using TwitchAssistant.UserInterface.ViewModels;

namespace TwitchAssistant.UserInterface.Views
{
    /// <summary>
    /// Логика взаимодействия для CoinSystemControlView.xaml
    /// </summary>
    public partial class CoinSystemControlView : UserControl
    {
        public CoinSystemControlView()
        {
            this.DataContext = new CoinSystemControlViewModel();
            InitializeComponent();
        }
    }
}
