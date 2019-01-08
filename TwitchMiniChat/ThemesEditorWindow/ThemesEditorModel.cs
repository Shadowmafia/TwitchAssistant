using System.Windows.Input;
using AssistantConfig;
using Tools.MVVMBaseClasses;

namespace TwitchMiniChat.ThemesEditorWindow
{
    class ThemesEditorModel : ViewModelBase
    {

        private ICommand _editorClosedCommand;
        public ICommand EditorClosedCommand
        {
            get
            {
                return _editorClosedCommand ?? (_editorClosedCommand = new Command((arg) => EditorClosed()));
            }
        }

        private void EditorClosed()
        {
            ConfigSet.SaveConfig();
        }
    }
}
