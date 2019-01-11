using System.Windows.Controls;
using TwitchAssistant.UserInterface.ViewModels;

namespace TwitchAssistant.UserInterface.Views
{
    /// <summary>
    /// Логика взаимодействия для DefailtCommandsView.xaml
    /// </summary>
    public partial class DefaultCommandsView : UserControl
    {
        public DefaultCommandsView()
        {
            DataContext = new DefaultCommandsViewModel();
            InitializeComponent();
        }
    }
}
