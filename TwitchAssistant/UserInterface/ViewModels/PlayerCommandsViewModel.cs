using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.CommandsSystem;
using TwitchBot.CommandsSystem.Commands;

namespace TwitchAssistant.UserInterface.ViewModels
{
    class PlayerCommandsViewModel
    {
        public ObservableCollection<PlayerBotCommand> Commands { get; set; } = CommandsController.PlayerCommandsList;
    }
}
