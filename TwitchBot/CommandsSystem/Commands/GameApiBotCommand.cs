using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses.Enums;
using DateBaseController.Models.CommandsModels;
using TwitchLib.Api.V5.Models.ChannelFeed;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    public class GameApiBotCommand : BotCommand<GameApiBotCommand>
    {
        public GameApiBotCommand(BaseCommand commandFromDataBase) : base(commandFromDataBase)
        {
        }

        public string Server { get; set; }
        public string NickName { get; set; }
     
    }
}
