using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses.Enums;
using TwitchLib.Api.V5.Models.ChannelFeed;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    public class GameApiBotCommand : BotCommand<GameApiBotCommand>
    {
        public GameApiBotCommand(string name, int id, string description, bool isEnabled, bool whisp, bool message, Action<ChatMessage, string, GameApiBotCommand> action, bool isUserLevelErrorResponse, TwitchRangs userLevel, TimeSpan globalCooldown, bool isGlobalCooldown, bool isGlobalErrorResponse) : base(name, id, description, isEnabled, whisp, message, action, isUserLevelErrorResponse, userLevel, globalCooldown, isGlobalCooldown, isGlobalErrorResponse)
        {
        }

        public string Server { get; set; }
        public string NickName { get; set; }
     
    }
}
