using System;
using System.Collections.ObjectModel;
using System.Linq;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem
{
    public class CommandsController
    {
        public ObservableCollection<IBotCommand> CommandList { get;} = new ObservableCollection<IBotCommand>();

        public void AddCommand(BotCommand command)
        {
            if((CommandList.FirstOrDefault(botCommand => botCommand.Id== command.Id)!=null))
            {
                return;
            }
            CommandList.Add(command);
        }

        public bool RemoveCommand(string commandName)
        {
            //if (CommandList.Remove(command => command.Name == commandName) > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}          
            return true;
        }

        public bool ExecuteCommandByName(ChatMessage user,string commandName,string commandBody=null)
        {
            try
            {
                IBotCommand x=CommandList.First(command => "!"+command.Name == commandName);
                if (x.IsEnabled)
                {
                    x.ExecuteCommand(user, commandBody);
                }              
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
          
        }
    }
}
