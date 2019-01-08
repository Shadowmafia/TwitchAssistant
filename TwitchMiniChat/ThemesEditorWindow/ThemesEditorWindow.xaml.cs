using System.Windows;
using TwitchMiniChat.ThemesEditorWindow.ThemesEditorControls;

namespace TwitchMiniChat.ThemesEditorWindow
{
    /// <summary>
    /// Логика взаимодействия для MiniChatView.xaml
    /// </summary>
    public partial class ThemesEditorWindow : Window
    {
        public ThemesEditorWindow(MiniChatWindow miniChatWindow)
        {                 
            this.DataContext= new ThemesEditorModel();        
           //miniChatEditor.TO      
            InitializeComponent();
            UserMiniChatEditorView miniChatEditor = new UserMiniChatEditorView(miniChatWindow);
            Body.Children.Add(miniChatEditor);

        }

      
    }
}
