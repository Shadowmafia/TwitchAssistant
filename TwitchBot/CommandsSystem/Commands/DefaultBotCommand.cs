using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses.Enums;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    public class DefaultBotCommand : BotCommand<DefaultBotCommand>
    {
        public DefaultBotCommand(string name, int id, string description, bool isEnabled, bool whisp, bool message, Action<ChatMessage, string, DefaultBotCommand> action, bool isUserLevelErrorResponse, TwitchRangs userLevel, TimeSpan globalCooldown, bool isGlobalCooldown, bool isGlobalErrorResponse) : base(name, id, description, isEnabled, whisp, message, action, isUserLevelErrorResponse, userLevel, globalCooldown, isGlobalCooldown, isGlobalErrorResponse)
        {
        }
    }
}
