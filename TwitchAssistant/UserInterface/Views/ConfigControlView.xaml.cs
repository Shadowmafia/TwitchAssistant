using System.Windows.Controls;
using TwitchAssistant.UserInterface.ViewModels;

namespace TwitchAssistant.UserInterface.Views
{
    /// <summary>
    /// Логика взаимодействия для ConfigViev.xaml
    /// </summary>
    public partial class ConfigControlView : UserControl
    {
        public ConfigControlView()
        {
           
            this.DataContext = ConfigControlViewModel.Instance;
            InitializeComponent();
        }

    }
}
