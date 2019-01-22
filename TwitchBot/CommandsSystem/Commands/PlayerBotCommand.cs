using System;
using DataClasses.Enums;
using DateBaseController.Models.CommandsModels;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    public class PlayerBotCommand : BotCommand<PlayerBotCommand>
    {
        public PlayerBotCommand(BaseCommand commandFromDataBase) : base(commandFromDataBase)
        {
        }
    }
}
