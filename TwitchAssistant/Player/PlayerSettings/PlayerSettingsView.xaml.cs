using System.Windows.Controls;

namespace TwitchAssistant.Player.PlayerSettings
{
    /// <summary>
    /// Логика взаимодействия для PlayerSettingsView.xaml
    /// </summary>
    public partial class PlayerSettingsView : UserControl
    {
        public PlayerSettingsView()
        {
            if(GlobalViewModels.PlayerSettingsViewModel == null)
            {
                GlobalViewModels.PlayerSettingsViewModel = new PlayerSettingsViewModel();
            }
            this.DataContext = GlobalViewModels.PlayerSettingsViewModel;
            InitializeComponent();
        }
    }
}
