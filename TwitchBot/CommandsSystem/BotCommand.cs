using System;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem
{
    public class BotCommand :IBotCommand
    {
        public string Name {get; set;}
        public int Id { get; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool Whisp { get; set; }
        public bool Message { get; set; }


        public Action<ChatMessage, string, BotCommand> _method { get; set; }

        public BotCommand(string name, int id, Action<ChatMessage,string,BotCommand> method)
        {
            Name = name;
            Id = id;
            _method = method;
            IsEnabled = true;
            Whisp = true;
            Message = true;
        }

        public void ExecuteCommand(ChatMessage viewer,string commandBody=null)
        {
           _method(viewer,commandBody,this);
        }
        
    }
}
