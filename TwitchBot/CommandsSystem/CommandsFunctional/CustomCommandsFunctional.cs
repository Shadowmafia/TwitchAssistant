using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.CommandsSystem.Commands;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.CommandsFunctional
{
    public static class CustomCommandsFunctional
    {
        public static Dictionary<string, Action<ChatMessage, string, CustomBotCommand>> Actions = new Dictionary<string, Action<ChatMessage, string, CustomBotCommand>>();
        static CustomCommandsFunctional()
        {
            Actions.Add("SendCustomCommandMessage", SendCustomCommandMessage);
        }

        public static void SendCustomCommandMessage(ChatMessage user, string body, CustomBotCommand command)
        {              
            Bot.CommandMessage(user.Username,command.ResponseMessage, command);
        }
    }
}
