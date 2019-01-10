using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DateBaseController;
using DateBaseController.Models;
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
                Viewer joinedUser = DbRepository.Instance.GetViewers().FirstOrDefault(viewer => viewer.Username == user.Username);
                if (joinedUser == null)
                {
                    var result = TwitchApiController.Api.V5.Users.GetUserByNameAsync(user.Username).Result;
                    int viewerId = int.Parse(result.Matches[0].Id);
                    joinedUser = new Viewer() { Id = viewerId, Username = user.Username, Rang = new Rang() { RangSets = new List<RangSet>() { new RangSet() } } };

                    DbRepository.Instance.GetViewers().Add(joinedUser);
                    joinedUser = DbRepository.Instance.GetViewers().Find((viewer => viewer.Id == joinedUser.Id));
                    joinedUser.LastCoinUpdate = DateTime.Now;
                    joinedUser.LastConnectToStream = DateTime.Now;
                    DbRepository.Instance.SaveChanges();
                    TwitchBotGlobalObjects.ChanelData.Watchers.Add(joinedUser);
                }
               

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
