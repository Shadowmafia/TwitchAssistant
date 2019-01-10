using System.Windows.Controls;
using TwitchAssistant.UserInterface.ViewModels;

namespace TwitchAssistant.UserInterface.Views
{
    /// <summary>
    /// Interaction logic for UsersGridView.xaml
    /// </summary>
    public partial class UsersGridView : UserControl
    {
        public UsersGridView()
        {
            this.DataContext = new UsersGridViewModel();
            InitializeComponent();
        }
    }
}
