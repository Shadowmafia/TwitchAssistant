using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses.Enums;
using DateBaseController.Models.CommandsModels;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    public class DefaultBotCommand : BotCommand<DefaultBotCommand>
    {
        public DefaultBotCommand(BaseCommand commandFromDataBase) : base(commandFromDataBase)
        {
        }
    }
}
