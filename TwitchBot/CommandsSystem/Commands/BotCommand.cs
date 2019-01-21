using DataClasses.Enums;
using DateBaseController;
using DateBaseController.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool IsUserLevelErrorResponse { get; set; }
        public TwitchRangs UserLevel { get; set; }

        public TimeSpan GlobalCooldown { get; set; }
        public bool IsGlobalCooldown { get; set; }
        public bool IsGlobalErrorResponse { get; set; }

        public bool IsWhispErrors { get; set; }
        public bool IsChatErrors { get; set; }

        public string Action { get; set; }
        /*
        public TimeSpan UserCooldown { get; set; }
        public bool IsUserCooldown { get; set; }
        public bool IsUserErrorResponse { get; set; }
        */

        protected DateTime LastCall { get; set; }   
        protected Action<ChatMessage, string, T> _method { get; set; }


        public BotCommand(string name, int id, string description, bool isEnabled, bool whisp, bool message, Action<ChatMessage, string, T> action,bool isUserLevelErrorResponse, TwitchRangs userLevel, TimeSpan globalCooldown, bool isGlobalCooldown,bool isGlobalErrorResponse)
        {
            Name = name;
            Id = id;
            Description = description;
            IsEnabled = isEnabled;
            Whisp = whisp;
            Message = message;
            _method = action;
            IsUserLevelErrorResponse = isUserLevelErrorResponse;
            UserLevel = userLevel;
            GlobalCooldown = globalCooldown;
            IsGlobalCooldown = isGlobalCooldown;
            IsGlobalErrorResponse = isGlobalErrorResponse;

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
            if (IsUserLevelErrorResponse&& canExecute==false)
            {
                TwitchBotGlobalObjects.Bot.SendMessage(errorResponse);
            }
            if (IsGlobalCooldown&& canExecute)
            {
                canExecute = CheckGlobalCooldown(ref errorResponse);
                if (IsGlobalErrorResponse && canExecute == false)
                {
                    TwitchBotGlobalObjects.Bot.SendMessage(errorResponse);
                }
            }        
            return canExecute;
        }

        private bool CheckUserLevel(Viewer user, ref string errorResponse)
        {
            string errString = $"Command error : 'You must be {UserLevel} or upper'";
            bool canExecute = false;
            if (user != null)
            {
                switch (UserLevel)
                {
                    case TwitchRangs.Unfollower:
                        canExecute = true;
                        break;
                    case TwitchRangs.Follower:
                        {
                            if (user.IsFollower ||user.IsSubscriber || user.IsModerator || user.IsBroadcaster )
                            {
                                canExecute = true;
                            }
                            else
                            {
                                canExecute = false;
                                errorResponse = errString;
                            }
                        }
                        break;
                    case TwitchRangs.Subscriber:
                        {
                            if (user.IsSubscriber || user.IsModerator || user.IsBroadcaster)
                            {
                                canExecute = true;
                            }
                            else
                            {
                                canExecute = false;
                                errorResponse = errString;
                            }
                        }
                        break;
                    case TwitchRangs.Moderator:
                        {
                            if (user.IsModerator || user.IsBroadcaster)
                            {
                                canExecute = true;
                            }
                            else
                            {
                                canExecute = false;
                                errorResponse = errString;
                            }
                        }
                        break;
                    case TwitchRangs.Broadcaster:
                        {
                            if (user.IsBroadcaster)
                            {
                                canExecute = true;
                            }
                            else
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
