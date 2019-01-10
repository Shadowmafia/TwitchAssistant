using System.Windows.Controls;

namespace TwitchAssistant.NoRefarcatorUi.BotCommandsControl.DefaultCommandsControl
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
