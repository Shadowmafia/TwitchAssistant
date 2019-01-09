using System.Windows.Controls;

namespace AssitantPlayer.PlayerSettings
{
    /// <summary>
    /// Логика взаимодействия для PlayerSettingsView.xaml
    /// </summary>
    public partial class PlayerSettingsView : UserControl
    {
        public PlayerSettingsView()
        {
            this.DataContext = PlayerSettingsViewModel.Instance;
            InitializeComponent();
        }
    }
}
