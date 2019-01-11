using System.Collections.ObjectModel;
using Tools.MVVMBaseClasses;
using TwitchBot.CommandsSystem;

namespace TwitchAssistant.UserInterface.ViewModels
{
    class DefaultCommandsViewModel:ViewModelBase
    {
      
       public ObservableCollection<IBotCommand> Commands { get; set; }=DefaultCommandsSet.Commands.CommandList;     
    }
}
