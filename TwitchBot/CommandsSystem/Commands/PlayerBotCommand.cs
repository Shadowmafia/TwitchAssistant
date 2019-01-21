using System;
using DataClasses.Enums;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    public class PlayerBotCommand : BotCommand<PlayerBotCommand>
    {
        public PlayerBotCommand(string name, int id, string description, bool isEnabled, bool whisp, bool message, Action<ChatMessage, string, PlayerBotCommand> action, bool isUserLevelErrorResponse, TwitchRangs userLevel, TimeSpan globalCooldown, bool isGlobalCooldown, bool isGlobalErrorResponse) : base(name, id, description, isEnabled, whisp, message, action, isUserLevelErrorResponse, userLevel, globalCooldown, isGlobalCooldown, isGlobalErrorResponse)
        {
        }
    }
}
