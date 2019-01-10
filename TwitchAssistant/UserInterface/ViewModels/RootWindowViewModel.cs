using System;
using System.Windows;
using System.Windows.Input;
using AssistantConfig;
using Tools.MVVMBaseClasses;
using TwitchMiniChat;

namespace TwitchAssistant.UserInterface.ViewModels
{
    class RootWindowViewModel : ViewModelBase
    {
    
        private bool _miniChatBtnOn { get; set; }
        public bool MiniChatBtnOn
        {
            get { return _miniChatBtnOn; }
            set
            {
                _miniChatBtnOn = value;
                OnPropertyChanged(nameof(MiniChatBtnOn));
            }
        }

        private ICommand _startMiniChatCommand;
        public ICommand StartMiniChatCommand
        {
            get
            {
                return _startMiniChatCommand ?? (_startMiniChatCommand = new Command((arg) => StartMiniChat()));
            }
        }

        private void StartMiniChat()
        {
            if (GlobalObjects.MiniChat == null)
            {
                GlobalObjects.MiniChat = new MiniChatWindow();
                GlobalObjects.MiniChat.Show();
                GlobalObjects.MiniChat.Closed += MiniChatClosed;
            }
            else
            {
                MessageBox.Show("chat is already running");
            }
        }
     
        private void MiniChatClosed(object sender, EventArgs e)
        {
            GlobalObjects.MiniChat = null;
            ConfigSet.SaveConfig();
        }
    }
}
