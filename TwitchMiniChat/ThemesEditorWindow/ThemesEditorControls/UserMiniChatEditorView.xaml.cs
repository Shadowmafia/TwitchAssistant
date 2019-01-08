using System.Windows.Controls;

namespace TwitchMiniChat.ThemesEditorWindow.ThemesEditorControls
{
    /// <summary>
    /// Логика взаимодействия для UserMiniChatEditorView.xaml
    /// </summary>
    public partial class UserMiniChatEditorView : UserControl
    {
        public UserMiniChatEditorView(MiniChatWindow miniChatWindow)
        {
            this.DataContext = new UserMiniChatEditorViewModel(miniChatWindow);
            InitializeComponent();
        }
    }
}
