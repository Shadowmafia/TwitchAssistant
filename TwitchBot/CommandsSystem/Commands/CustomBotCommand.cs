using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    class CustomBotCommand : BotCommand<CustomBotCommand>
    {
        public string ResponseMessage { get; set; }
        public CustomBotCommand(string name, int id, string description, bool isEnabled, bool whisp, bool message, Action<ChatMessage, string, CustomBotCommand> action,string responseMessage) : base(name, id, description, isEnabled, whisp, message, action)
        {
            ResponseMessage = responseMessage;
        }
    }
}
