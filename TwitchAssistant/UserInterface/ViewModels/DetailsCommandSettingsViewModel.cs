using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using DataClasses.Enums;
using Tools.MVVMBaseClasses;
using TwitchBot.CommandsSystem.Commands;

namespace TwitchAssistant.UserInterface.ViewModels
{
    public  class DetailsCommandSettingsViewModel<T> : ViewModelBase where T : BotCommand<T>
    {
       
        private BotCommand<T> _botCommand;
        public BotCommand<T> BotCommand
        {
            get { return _botCommand; }
            set
            {
                _botCommand = value;
                OnPropertyChanged(nameof(BotCommand));
            }
        } 

 
        public DetailsCommandSettingsViewModel(T command)
        {
            BotCommand = command;
          
        }
    }
}
