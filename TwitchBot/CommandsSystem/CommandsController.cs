﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DateBaseController;
using DateBaseController.Models;
using DateBaseController.Models.CommandsModels;
using TwitchBot.CommandsSystem.Commands;
using TwitchBot.CommandsSystem.CommandsFunctional;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem
{
    public static class CommandsController
    {
        public static ObservableCollection<DefaultBotCommand> DefaultCommandsList = new ObservableCollection<DefaultBotCommand>();
        public static ObservableCollection<PlayerBotCommand> PlayerCommandsList = new ObservableCollection<PlayerBotCommand>();

        static CommandsController()
        {
            InitAllCommands();
        }
        public static bool ExecuteCommandByName(ChatMessage user,string commandName,string commandBody=null)
        {         
            try
            {
                CheckAndAddOrEditUserIfNeed(user);
                DefaultBotCommand xBotCommand = DefaultCommandsList.FirstOrDefault(command => "!" + command.Name == commandName);
                TryExecuteCommand(user,commandBody, xBotCommand);
                PlayerBotCommand xPlayerCommand = PlayerCommandsList.FirstOrDefault(command => "!" + command.Name == commandName);
                TryExecuteCommand(user, commandBody, xPlayerCommand);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
          
        }
        private static void TryExecuteCommand<T>(ChatMessage user,string commandBody, T command) where T : BotCommand<T>
        {
            if (command != null)
            {
                if (command.IsEnabled)
                {
                    command.ExecuteCommand(user,commandBody);                
                }
            }
        }
        private static void CheckAndAddOrEditUserIfNeed(ChatMessage user)
        {
           
            Viewer joinedUser = AssistantDb.Instance.Viewers.GetAll().FirstOrDefault(viewer => viewer.Username == user.Username);           
            if (joinedUser == null)
            {
                var result = TwitchApiController.Api.V5.Users.GetUserByNameAsync(user.Username).Result;
                int viewerId = int.Parse(result.Matches[0].Id);
                joinedUser = new Viewer() { Id = viewerId, Username = user.Username, Rang = new Rang() { RangSets = new List<RangSet>() { new RangSet() } } };
                joinedUser.IsModerator = user.IsModerator;
                joinedUser.IsSubscriber = user.IsSubscriber;
                joinedUser.IsBroadcaster = user.IsBroadcaster;
                var x = TwitchBotGlobalObjects.ChanelData.Followers.FirstOrDefault(tmpUser => tmpUser.User.Id == user.UserId);
                if (x != null)
                {
                    joinedUser.IsFollower = true;
                }
                AssistantDb.Instance.Viewers.Add(joinedUser);
                joinedUser = AssistantDb.Instance.Viewers.Get(joinedUser.Id);
                joinedUser.LastCoinUpdate = DateTime.Now;
                joinedUser.LastConnectToStream = DateTime.Now;
                AssistantDb.Instance.SaveChanges();
                TwitchBotGlobalObjects.ChanelData.Watchers.Add(joinedUser);
            }
            else
            {
                joinedUser.IsBroadcaster = user.IsBroadcaster;
                joinedUser.IsModerator = user.IsModerator;
                joinedUser.IsSubscriber = user.IsSubscriber;
                var x = TwitchBotGlobalObjects.ChanelData.Followers.FirstOrDefault(tmpUser => tmpUser.User.Id == user.UserId);
                if (x != null)
                {
                    joinedUser.IsFollower = true;
                }
                else
                {
                    joinedUser.IsFollower = false;
                }
            }
        }

        private static void InitAllCommands()
        {          
            List<DefaultCommand> defaultCommands = AssistantDb.Instance.DefaultCommands.GetAll().ToList();
            foreach (var command in defaultCommands)
            {
                TimeSpan cooldown = new TimeSpan(command.GlobalCooldown);
                DefaultBotCommand newCommand = new DefaultBotCommand(command);
                newCommand.SetNewAction(DefaultCommandsFunctional.Actions[command.Action]);
                DefaultCommandsList.Add(newCommand);
            }
            
            List<PlayerCommand> playerCommands = AssistantDb.Instance.PlayerCommands.GetAll().ToList();
            foreach (var command in playerCommands)
            {
                PlayerBotCommand newCommand = new PlayerBotCommand(command);
                newCommand.SetNewAction(PlayerCommandsFunctional.Actions[command.Action]);
                PlayerCommandsList.Add(newCommand);               
            }
        }
        public static void SaveCommands()
        {
            List<DefaultCommand> defaultCommands = new List<DefaultCommand>();
            foreach (var command in DefaultCommandsList)
            {           
                defaultCommands.Add(new DefaultCommand()
                {
                    Id = command.Id,
                    Name = command.Name,
                    Message = command.Message,
                    IsEnabled = command.IsEnabled,
                    Whisp = command.Whisp,
                    Description = command.Description,
                    IsUserLevelErrorResponse = command.IsUserLevelErrorResponse,
                    UserLevel = command.UserLevel,
                    GlobalCooldown = command.GlobalCooldown.Ticks,
                    IsGlobalCooldown = command.IsGlobalCooldown,
                    IsGlobalErrorResponse = command.IsUserLevelErrorResponse,
                    IsWhispErrors = command.IsWhispErrors,
                    IsChatErrors = command.IsChatErrors,
                    Action = command.Action
                });
            }          
            AssistantDb.Instance.DefaultCommands.AddOrUpdate(defaultCommands);
            
          

            List<PlayerCommand> playerCommands = new List<PlayerCommand>();
            foreach (var command in PlayerCommandsList)
            {
                playerCommands.Add(new PlayerCommand()
                {
                    Id = command.Id,
                    Name = command.Name,
                    Message = command.Message,
                    IsEnabled = command.IsEnabled,
                    Whisp = command.Whisp,
                    Description = command.Description,
                    IsUserLevelErrorResponse = command.IsUserLevelErrorResponse,
                    UserLevel = command.UserLevel,
                    GlobalCooldown = command.GlobalCooldown.Ticks,
                    IsGlobalCooldown = command.IsGlobalCooldown,
                    IsGlobalErrorResponse = command.IsUserLevelErrorResponse,
                    IsWhispErrors = command.IsWhispErrors,
                    IsChatErrors = command.IsChatErrors,
                    Action = command.Action
                });
            }
            AssistantDb.Instance.PlayerCommands.AddOrUpdate(playerCommands);
            

            AssistantDb.Instance.SaveChanges();
        }
    }
}
