using DataClasses.Enums;
using DateBaseController;
using DateBaseController.Models;
using DateBaseController.Models.CommandsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TwitchLib.Client.Models;

namespace TwitchBot.CommandsSystem.Commands
{
    public abstract class BotCommand<T> where T : class
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsWhisp { get; set; }
        public bool IsMessage { get; set; }

        public bool IsUserLevelErrorResponse { get; set; }
        public TwitchRangs UserLevel { get; set; }

        public TimeSpan GlobalCooldown { get; set; }
        public bool IsGlobalCooldown { get; set; }
        public bool IsGlobalErrorResponse { get; set; }

        public bool IsWhispErrors { get; set; }
        public bool IsChatErrors { get; set; }

        public string ActionName { get; set; }

        /*
        public TimeSpan UserCooldown { get; set; }
        public bool IsUserCooldown { get; set; }
        public bool IsUserErrorResponse { get; set; }
        */

        protected DateTime LastCall { get; set; }
        protected Action<ChatMessage, string, T> _method { get; set; } 

        public BotCommand()
        {

        }
        public BotCommand(BaseCommand commandFromDataBase)
        {
            Name = commandFromDataBase.Name;
            Id = commandFromDataBase.Id;
            Description = commandFromDataBase.Description;
            IsEnabled = commandFromDataBase.IsEnabled;
            IsWhisp = commandFromDataBase.Whisp;
            IsMessage = commandFromDataBase.Message;

            IsUserLevelErrorResponse = commandFromDataBase.IsUserLevelErrorResponse;
            UserLevel = commandFromDataBase.UserLevel;

            GlobalCooldown = new TimeSpan(commandFromDataBase.GlobalCooldown);
            IsGlobalCooldown = commandFromDataBase.IsGlobalCooldown;
            IsGlobalErrorResponse = commandFromDataBase.IsGlobalErrorResponse;

            IsWhispErrors = commandFromDataBase.IsWhispErrors;
            IsChatErrors = commandFromDataBase.IsChatErrors;

            ActionName = commandFromDataBase.Action;
            LastCall = DateTime.Now - new TimeSpan(10, 0, 0);
        }
        public void SetNewAction(Action<ChatMessage, string, T> action)
        {
            _method = action;
        }
        public void ExecuteCommand(ChatMessage viewer, string commandBody = null)
        {
            if (this is T)
            {
                if (CanExecute(viewer))
                {
                    _method(viewer, commandBody, this as T);
                    LastCall = DateTime.Now;
                }
            }
        }
        private bool CanExecute(ChatMessage viewer)
        {
            string errorResponse = "";
            bool canExecute = false;
            List<Viewer> vievers = AssistantDb.Instance.Viewers.GetAll().ToList();
            Viewer user = vievers.FirstOrDefault(watcher => watcher.Username == viewer.Username);

            canExecute = CheckUserLevel(user, ref errorResponse);
            if (IsUserLevelErrorResponse && canExecute == false)
            {
                if (IsWhispErrors)
                {
                    TwitchBotGlobalObjects.Bot.WhispMessage(viewer.Username, errorResponse);                 
                }
                if (IsChatErrors)
                {
                    TwitchBotGlobalObjects.Bot.SendMessage(errorResponse);
                }
               
            }
            if (IsGlobalCooldown && canExecute)
            {
                canExecute = CheckGlobalCooldown(ref errorResponse);
                if (IsGlobalErrorResponse && canExecute == false)
                {
                    if (IsWhispErrors)
                    {
                        TwitchBotGlobalObjects.Bot.WhispMessage(viewer.Username, errorResponse);
                    }
                    if (IsChatErrors)
                    {
                        TwitchBotGlobalObjects.Bot.SendMessage(errorResponse);
                    }
                }
            }
            return canExecute;
        }

        private bool CheckUserLevel(Viewer user, ref string errorResponse)
        {
            string errString = $"Command error : 'You must be {UserLevel} or upper'";
            bool canExecute = true;
            if (user != null)
            {
                switch (UserLevel)
                {
                    case TwitchRangs.Unfollower:
                        canExecute = true;
                        break;
                    case TwitchRangs.Follower:
                        {
                            if (!(user.IsFollower || user.IsSubscriber || user.IsModerator || user.IsBroadcaster))
                            {
                                canExecute = false;
                                errorResponse = errString;
                            }
                        }
                        break;
                    case TwitchRangs.Subscriber:
                        {
                            if (!(user.IsSubscriber || user.IsModerator || user.IsBroadcaster))
                            {
                                canExecute = false;
                                errorResponse = errString;
                            }
                        }
                        break;
                    case TwitchRangs.Moderator:
                        {
                            if (!(user.IsModerator || user.IsBroadcaster))
                            {
                                canExecute = false;
                                errorResponse = errString;
                            }
                        }
                        break;
                    case TwitchRangs.Broadcaster:
                        {
                            if (!user.IsBroadcaster)
                            {
                                canExecute = false;
                                errorResponse = "Command error : ' You must be broadcaster'";
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return canExecute;
        }
        private bool CheckGlobalCooldown(ref string errorResponse)
        {
            bool canExecute = false;
            if (DateTime.Now < LastCall + GlobalCooldown)
            {
                errorResponse = $"Command global cooldown : next possible call {(LastCall + GlobalCooldown):hh\\:mm\\:ss}";
            }
            else
            {
                canExecute = true;
            }
            return canExecute;
        }

    }
}
