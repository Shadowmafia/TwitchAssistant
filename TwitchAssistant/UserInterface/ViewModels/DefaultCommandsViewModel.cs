using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AssistantConfig;
using DataClasses.Enums;
using Tools.MVVMBaseClasses;
using TwitchBot.CommandsSystem;
using TwitchBot.CommandsSystem.Commands;

namespace TwitchAssistant.UserInterface.ViewModels
{
    class DefaultCommandsViewModel:ViewModelBase
    {
        public ObservableCollection<DefaultBotCommand> Commands { get; set; } = CommandsController.DefaultCommandsList;    
    }
}
