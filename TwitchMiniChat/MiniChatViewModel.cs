using System.Windows;
using System.Windows.Input;
using AssistantConfig;
using CefSharp;
using Tools.MVVMBaseClasses;
using TwitchMiniChat.ThemesEditorWindow;
using TwitchMiniChat.ThemesEditorWindow.ThemesEditorControls;

namespace TwitchMiniChat
{
    public class MiniChatViewModel : ViewModelBase
    {
        private MiniChatWindow _miniChatWindow;
        private AppearanceJavaScriptsExecuter _javaScriptsExecuter;
        public  MiniChatViewModel(MiniChatWindow miniChatWindow)
        {
            _miniChatWindow = miniChatWindow;          
        }
        private bool _isBrowserLoaded =false;
        //Browser settings___________________________________________________
        private string _chatRequestUrl;
        public string ChatRequestUrl
        {
            get { return _chatRequestUrl; }
            set
            {
                _chatRequestUrl = value;
                OnPropertyChanged(nameof(ChatRequestUrl));
            }
        }
        //Window settings____________________________________________________

        public double Width
        {
            get { return ConfigSet.Config.MiniChatConfig.ChatSize.Width; }
            set
            {
                ConfigSet.Config.MiniChatConfig.ChatSize.Width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        public double Height
        {
            get { return ConfigSet.Config.MiniChatConfig.ChatSize.Height; }
            set
            {
                ConfigSet.Config.MiniChatConfig.ChatSize.Height = value;

                OnPropertyChanged(nameof(Height));
            }
        }
        public double Top
        {
            get { return ConfigSet.Config.MiniChatConfig.ChatPosition.Y; }
            set
            {
                ConfigSet.Config.MiniChatConfig.ChatPosition.Y = value;
                OnPropertyChanged(nameof(Top));
            }
        } 
        public double Left
        {
            get { return ConfigSet.Config.MiniChatConfig.ChatPosition.X; }
            set
            {
                ConfigSet.Config.MiniChatConfig.ChatPosition.X = value;
                OnPropertyChanged(nameof(Left));
            }
        }

        //Chat settings______________________________________________________

        public bool ChatSettingsAndInput
        {
            get { return ConfigSet.Config.MiniChatConfig.ChatSettingsAndInput; }
            set
            {
                ConfigSet.Config.MiniChatConfig.ChatSettingsAndInput = value;
                if (_isBrowserLoaded)
                {
                    ShowHideInputAndSettings();
                }            
                OnPropertyChanged(nameof(ChatSettingsAndInput));
            }
        }
        public bool Topmost
        {
            get { return ConfigSet.Config.MiniChatConfig.Topmost;}
            set
            {
                ConfigSet.Config.MiniChatConfig.Topmost = value;
                OnPropertyChanged(nameof(Topmost));
            }
        }

        public bool IsLocked
        {
            get { return ConfigSet.Config.MiniChatConfig.IsLocked; }
            set
            {
                if (value)
                {
                    ChatStyle = (Style)_miniChatWindow.FindResource("NoneWindowStyle");
                    Top += 26;
                    Left += 1;
                    Height -= 27;
                    Width -= 2;               
                }
                else
                {
                    ChatStyle = (Style)_miniChatWindow.FindResource("CustomWindowStyle");
                    Top -= 26;
                    Left -= 1;
                    Height += 27;
                    Width += 2;                  
                }
                ConfigSet.Config.MiniChatConfig.IsLocked = value;
                OnPropertyChanged(nameof(IsLocked));
            }
        }
        private Style _chatStyle { get; set; }
        public Style ChatStyle
        {
            get { return _chatStyle; }
            set
            {
                _chatStyle = value;
                OnPropertyChanged(nameof(ChatStyle));
            }
        }
        private void LoadLock()
        {     
                if (IsLocked)
                {
                    ChatStyle = (Style)_miniChatWindow.FindResource("NoneWindowTemplate");              
                }
                else
                {
                    ChatStyle = (Style)_miniChatWindow.FindResource("CustomWindowStyle");
                }
                OnPropertyChanged(nameof(IsLocked));
        }
    
        public bool ChatUsage
        {
            get { return ConfigSet.Config.MiniChatConfig.ChatUsage; }
            set
            {
                if (value)
                {
                    LayoutVisibility = Visibility.Collapsed;
                }
                else
                {
                    LayoutVisibility = Visibility.Visible;
                }

                ConfigSet.Config.MiniChatConfig.ChatUsage = value;
                OnPropertyChanged(nameof(ChatUsage));
            }
        }
        Visibility _layoutVisibility { get; set; }
        public Visibility LayoutVisibility
        {
            get { return _layoutVisibility; }
            set
            {
                _layoutVisibility = value;
                OnPropertyChanged(nameof(LayoutVisibility));
            }
        }

          
        //Chat commands_______________________________________________________
        private ICommand _chatLoadedCommand;
        public ICommand ChatLoadedCommand
        {
            get
            {
                return _chatLoadedCommand ?? (_chatLoadedCommand = new Command((arg) => LoadSettings()));
            }
        }
        private void LoadSettings()
        {
            LoadLock();
            _javaScriptsExecuter = new AppearanceJavaScriptsExecuter(_miniChatWindow.ChatBrowser);
            ChatUsage = ConfigSet.Config.MiniChatConfig.ChatUsage;
            ChatRequestUrl = $"https://www.twitch.tv/popout/{ ConfigSet.Config.BotConfig.StreamName }/chat?popout=";

            _miniChatWindow.ChatBrowser.LoadingStateChanged += (sender, args) =>
            {
                _isBrowserLoaded = false;
                //Wait for the Page to finish loading
                if (args.IsLoading == false)
                {
                    _isBrowserLoaded = true;
                    if (!ChatSettingsAndInput)
                    {
                        _javaScriptsExecuter.HideChatSettingsAndInputScriptWithDeley();
                    }
                    _javaScriptsExecuter.PreparationForBackGround();
                    UserMiniChatEditorViewModel tmp = new UserMiniChatEditorViewModel(_miniChatWindow);
                    tmp.ChangeBackgroundColor();
                }
            };

        }

        private ICommand _chatClosedCommand;
        public ICommand ChatClosedCommand
        {
            get
            {
                return _chatClosedCommand ?? (_chatClosedCommand = new Command((arg) => SaveSettings()));
            }
        }
        private void SaveSettings()
        {
            ConfigSet.SaveConfig();
        }

        private ICommand _lockUnlockChatCommand;
        public ICommand LockUnlockChatCommand
        {
            get
            {
                return _lockUnlockChatCommand ?? (_lockUnlockChatCommand = new Command((arg) => LockUnlockChat()));
            }
        }
        private void LockUnlockChat()
        {
            IsLocked = !IsLocked;
        }

        private ICommand _usabelUnusableCommand;
        public ICommand UsabelUnusableCommand
        {
            get
            {
                return _usabelUnusableCommand ?? (_usabelUnusableCommand = new Command((arg) => UsabelUnusableChat()));
            }
        }
        private void UsabelUnusableChat()
        {
            ChatUsage = !ChatUsage;
        }

        private ICommand _topmostCommand;
        public ICommand TopmostCommand
        {
            get
            {
                return _topmostCommand ?? (_topmostCommand = new Command((arg) => ChatTopmost()));
            }
        }
        private void ChatTopmost()
        {
            Topmost = !Topmost;
        }

        private ICommand _onnOffSettingsAndInputCommand;
        public ICommand OnnOffSettingsAndInputCommand
        {
            get
            {
                return _onnOffSettingsAndInputCommand ?? (_onnOffSettingsAndInputCommand = new Command((arg) => OnnOffChatSettingsAndInput()));
            }
        }
        private void OnnOffChatSettingsAndInput()
        {          
            ChatSettingsAndInput = !ChatSettingsAndInput;
        }
        private void ShowHideInputAndSettings()
        {
            if (ChatSettingsAndInput)
            {
                _javaScriptsExecuter.ShowChatSettingsAndInputScript();
            }
            else
            {
                _javaScriptsExecuter.HideChatSettingsAndInputScript();

            }
        } 
        private ICommand _showChatSettingsCommand;
        public ICommand ShowChatSettingsCommand
        {
            get
            {
                return _showChatSettingsCommand ?? (_showChatSettingsCommand = new Command((arg) => ShowChatSettings()));
            }
        }

        private  void ShowChatSettings()
        {
            _miniChatWindow.ChatSettingsElement.IsEnabled = !_miniChatWindow.ChatSettingsElement.IsEnabled;
        }

        private ICommand _startThemesEditor;
        public ICommand StartThemesEditor
        {
            get
            {
                return _startThemesEditor ?? (_startThemesEditor = new Command((arg) => ShowThemesEditor()));
            }
        }

        private void ShowThemesEditor()
        {
            ThemesEditorWindow.ThemesEditorWindow themesWindow = new ThemesEditorWindow.ThemesEditorWindow(_miniChatWindow);
            themesWindow.ShowDialog();
        }
    }
}
