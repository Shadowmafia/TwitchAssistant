using System.Windows.Input;
using System.Windows.Media;
using AssistantConfig;
using CefSharp;
//using CefSharp;
using Tools.MVVMBaseClasses;

namespace TwitchMiniChat.ThemesEditorWindow.ThemesEditorControls
{
    public class UserMiniChatEditorViewModel:ViewModelBase
    {
        private readonly MiniChatWindow _miniChatWindow;
        private AppearanceJavaScriptsExecuter javaScriptsExecuter;
        public UserMiniChatEditorViewModel(MiniChatWindow miniChatWindow)
        {
            _miniChatWindow = miniChatWindow;
            javaScriptsExecuter = new AppearanceJavaScriptsExecuter(miniChatWindow.ChatBrowser);
        }
        public Color BgColor
        {
            get { return ConfigSet.Config.MiniChatConfig.BgColor; }
            set
            {
                ConfigSet.Config.MiniChatConfig.BgColor = value;
                OnPropertyChanged(nameof(BgColor));
            }
        }
   
        private ICommand _bgColorChangeCommand;
        public ICommand BgColorChangeCommand
        {
            get
            {
                return _bgColorChangeCommand ?? (_bgColorChangeCommand = new Command((arg) => ChangeBackgroundColor()));
            }
        }  
        public void ChangeBackgroundColor()
        {
            if (BgColor == Color.FromArgb(255, 239, 238, 241))
            {
                return;             
            }
            double opacity = (double)BgColor.A / 255;
            opacity = (double)((int)(opacity *= 100)) / 100;
            javaScriptsExecuter.ChangeBackGroundColorScript(BgColor.R.ToString(), BgColor.G.ToString(), BgColor.B.ToString(), opacity.ToString().Replace(',', '.'));      
        }

        private ICommand _defaultSettingsCommand;
        public ICommand DefaultSettingsCommand
        {
            get
            {
                return _defaultSettingsCommand ?? (_defaultSettingsCommand = new Command((arg) => DefaultSettings()));
            }
        }
        private void DefaultSettings()
        {
            BgColor = Color.FromArgb(255, 239, 238, 241);
            javaScriptsExecuter = new AppearanceJavaScriptsExecuter(_miniChatWindow.ChatBrowser);
            _miniChatWindow.ChatBrowser.Reload();
        }
    }
}
