using System.Windows.Controls;
using TwitchAssistant.UserInterface.ViewModels;

namespace TwitchAssistant.UserInterface.Views
{
    /// <summary>
    /// Логика взаимодействия для GeneralControlView.xaml
    /// </summary>
    public partial class GeneralControlView : UserControl
    {
        public GeneralControlView()
        {
            this.DataContext = ConfigControlViewModel.Instance;
            InitializeComponent();
        }
    }
}
