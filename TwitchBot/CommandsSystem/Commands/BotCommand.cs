using System;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    public class BotCommand<T> where T : class
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool Whisp { get; set; }
        public bool Message { get; set; }

        protected Action<ChatMessage, string,T> _method { get; set; }
        public  BotCommand(string name,int id,string description,bool isEnabled,bool whisp,bool message, Action<ChatMessage, string,T> action)
        {
            Name = name;
            Id = id;
            Description = description;
            IsEnabled = isEnabled;
            Whisp = whisp;
            Message = message;
            _method = action;
        }
        public void SetNewAction(Action<ChatMessage, string, T> action)
        {
            _method = action;
        }
        public  void ExecuteCommand(ChatMessage viewer, string commandBody = null)
        {
            if(this is T)
            {
                _method(viewer, commandBody, this as T);
            }           
        }
       
    }
}
