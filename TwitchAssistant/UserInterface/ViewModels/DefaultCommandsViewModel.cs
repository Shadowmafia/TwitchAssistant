using System.Collections.ObjectModel;
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
