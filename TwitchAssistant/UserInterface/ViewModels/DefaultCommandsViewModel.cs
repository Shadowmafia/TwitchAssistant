using System.Collections.ObjectModel;
using Tools.MVVMBaseClasses;
using TwitchBot.CommandsSystem;

namespace TwitchAssistant.NoRefarcatorUi.BotCommandsControl.DefaultCommandsControl
{
    class DefaultCommandsViewModel:ViewModelBase
    {
      
       public ObservableCollection<IBotCommand> Commands { get; set; }=DefaultCommandsSet.Commands.CommandList;     
    }
}
