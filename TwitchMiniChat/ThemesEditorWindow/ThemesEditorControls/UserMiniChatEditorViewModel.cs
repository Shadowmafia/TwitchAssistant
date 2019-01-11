using System.Windows.Input;
using System.Windows.Media;
using AssistantConfig;
//using CefSharp;
using Tools.MVVMBaseClasses;

namespace TwitchMiniChat.ThemesEditorWindow.ThemesEditorControls
{
    public class UserMiniChatEditorViewModel:ViewModelBase
    {
        private readonly MiniChatWindow _miniChatWindow;
        public UserMiniChatEditorViewModel(MiniChatWindow miniChatWindow)
        {
            _miniChatWindow = miniChatWindow;
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
            string changeColorScript = $@"
            var ChatBody = document.getElementsByTagName('body');
            ChatBody[0].setAttribute('style', 'background:rgba({BgColor.R},{BgColor.G},{BgColor.B},{opacity.ToString().Replace(',', '.')}) !important');

            var popout = document.getElementsByClassName('popout-chat-page');
            popout[0].setAttribute('style', 'background:rgba(255,255,255,0) !important');
            var chatElements = document.getElementsByClassName('tw-c-background-alt');
            chatElements[2].setAttribute('style', 'background:rgba(255,255,255,0) !important');
            ";

           // _miniChatWindow.ChatBrowser.ExecuteScriptAsync(changeColorScript);
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
           // _miniChatWindow.ChatBrowser.Reload();
        }
    }
}
