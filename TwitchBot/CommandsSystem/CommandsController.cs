using System;
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
        public static ObservableCollection<CustomBotCommand> CustomCommandsList = new ObservableCollection<CustomBotCommand>();
        static CommandsController()
        {
            InitAllCommands();
        }
        public static bool ExecuteCommandByName(ChatMessage user,string commandName,string commandBody=null)
        {         
            try
            {
                TwitchBotGlobalObjects.CheckAndAddOrEditUserIfNeed(user);
                DefaultBotCommand xBotCommand = DefaultCommandsList.FirstOrDefault(command => "!" + command.Name == commandName);
                TryExecuteCommand(user,commandBody, xBotCommand);
                PlayerBotCommand xPlayerCommand = PlayerCommandsList.FirstOrDefault(command => "!" + command.Name == commandName);
                TryExecuteCommand(user, commandBody, xPlayerCommand);

                CustomBotCommand xCustomCommand = CustomCommandsList.FirstOrDefault(command => "!" + command.Name == commandName);
                TryExecuteCommand(user, commandBody, xCustomCommand);
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


        private static void InitAllCommands()
        {          
            List<DefaultCommand> defaultCommands = AssistantDb.Instance.DefaultCommands.GetAll().ToList();
            foreach (var command in defaultCommands)
            {             
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

            List<CustomCommand> customCoommands = AssistantDb.Instance.CustomCommands.GetAll().ToList();
            foreach (var command in customCoommands)
            {
                CustomBotCommand newCommand = new CustomBotCommand(command);
                newCommand.SetNewAction(CustomCommandsFunctional.Actions[command.Action]);
                newCommand.ResponseMessage = command.ResponseMessage;
                CustomCommandsList.Add(newCommand);
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
                    Message = command.IsMessage,
                    IsEnabled = command.IsEnabled,
                    Whisp = command.IsWhisp,
                    Description = command.Description,
                    IsUserLevelErrorResponse = command.IsUserLevelErrorResponse,
                    UserLevel = command.UserLevel,
                    GlobalCooldown = command.GlobalCooldown.Ticks,
                    IsGlobalCooldown = command.IsGlobalCooldown,
                    IsGlobalErrorResponse = command.IsUserLevelErrorResponse,
                    IsWhispErrors = command.IsWhispErrors,
                    IsChatErrors = command.IsChatErrors,
                    Action = command.ActionName
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
                    Message = command.IsMessage,
                    IsEnabled = command.IsEnabled,
                    Whisp = command.IsWhisp,
                    Description = command.Description,
                    IsUserLevelErrorResponse = command.IsUserLevelErrorResponse,
                    UserLevel = command.UserLevel,
                    GlobalCooldown = command.GlobalCooldown.Ticks,
                    IsGlobalCooldown = command.IsGlobalCooldown,
                    IsGlobalErrorResponse = command.IsUserLevelErrorResponse,
                    IsWhispErrors = command.IsWhispErrors,
                    IsChatErrors = command.IsChatErrors,
                    Action = command.ActionName
                });
            }
            AssistantDb.Instance.PlayerCommands.AddOrUpdate(playerCommands);


            List<CustomCommand> customCommands = new List<CustomCommand>();
            foreach (var command in CustomCommandsList)
            {
                customCommands.Add(new CustomCommand()
                {
                    Id = command.Id,
                    Name = command.Name,
                    Message = command.IsMessage,
                    IsEnabled = command.IsEnabled,
                    Whisp = command.IsWhisp,
                    Description = command.Description,
                    IsUserLevelErrorResponse = command.IsUserLevelErrorResponse,
                    UserLevel = command.UserLevel,
                    GlobalCooldown = command.GlobalCooldown.Ticks,
                    IsGlobalCooldown = command.IsGlobalCooldown,
                    IsGlobalErrorResponse = command.IsUserLevelErrorResponse,
                    IsWhispErrors = command.IsWhispErrors,
                    IsChatErrors = command.IsChatErrors,
                    Action = command.ActionName,
                    ResponseMessage = command.ResponseMessage                 
                });
            }
            AssistantDb.Instance.CustomCommands.AddOrUpdate(customCommands);


            AssistantDb.Instance.SaveChanges();
        }
    }
}
