using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.V5.Models.ChannelFeed;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    class GameApiBotCommand : BotCommand<GameApiBotCommand>
    {
        public string Server { get; set; }
        public string NickName { get; set; }
        public GameApiBotCommand(string name, int id, string description, bool isEnabled, bool whisp, bool message, Action<ChatMessage, string, GameApiBotCommand> action,string server,string nickName) : base(name, id, description, isEnabled, whisp, message, action)
        {
            Server = server;
            NickName = nickName;
        }
    }
}
