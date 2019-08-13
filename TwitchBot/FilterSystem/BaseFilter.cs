using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using DataClasses.Enums;
using DateBaseController;
using DateBaseController.Models;
using TwitchBot.FilterSystem;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;

namespace TwitchBot.ChatFilter
{
    public abstract class BaseFilter
    {
        public bool IsEnabled { get; set; }
        public bool IsTimeOut { get; set; }
        public bool IsChatResponce { get; set; }
        public bool IsWhisResponce { get; set; }
        public bool IsUserName { get; set; }
        public int CountingExecuteBeforeSanctions{ get; set; }
        public int CountingExecuteBeforeTimeOut { get; set; }
        public string WarningMessage { get; set; }
        public string TimeOutMessage { get; set; }
        public TimeSpan UserTimeOutDuration { get; set; }
        public TwitchRangs MinIgnoreUserRang { get; set; }
        
        protected List<UserActionInFilter> _userActionInFilters { get; set; } = new List<UserActionInFilter>();

        protected ChatMessage _message { get; set; }
       
        public void ExecuteFilter(ChatMessage message)
        {
            _message = message;

            if (IsEnabled && FilterCheckAction())
            {
                FilterTrigerred();
            }
        }

        protected abstract bool FilterCheckAction();

        protected void FilterTrigerred()
        {
            bool isUseActions = false;

            TwitchBotGlobalObjects.CheckAndAddOrEditUserIfNeed(this._message);
            Viewer curentUser = AssistantDb.Instance.Viewers.Get(Convert.ToInt32(_message.UserId));

            switch (this.MinIgnoreUserRang)
            {
                case TwitchRangs.Unfollower:
                    isUseActions = true;
                    break;
                case TwitchRangs.Follower:
                {
                    if (!(curentUser.IsFollower || curentUser.IsSubscriber || curentUser.IsModerator || curentUser.IsBroadcaster))
                    {
                        isUseActions = true;
                    }
                }
                    break;
                case TwitchRangs.Subscriber:
                {
                    if (!(curentUser.IsSubscriber || curentUser.IsModerator || curentUser.IsBroadcaster))
                    {
                        isUseActions = true;
                    }
                }
                    break;
                case TwitchRangs.Moderator:
                {
                    if (!(curentUser.IsModerator || curentUser.IsBroadcaster))
                    {
                        isUseActions = true;
                    }
                }
                    break;
                case TwitchRangs.Broadcaster:
                {
                    if (!curentUser.IsBroadcaster)
                    {
                        isUseActions = true;
                    }
                }
                    break;
                default:
                    break;
            }

            if (isUseActions)
            {
                UserActionInFilter currentUser = this._userActionInFilters.FirstOrDefault(user => user.User.Username == this._message.Username);

                if (currentUser == null)
                {
                    var tmpUser = new UserActionInFilter()
                    {
                        User = this._message
                    };

                    currentUser = tmpUser;
                    _userActionInFilters.Add(currentUser);
                };

                currentUser.CountExecuting++;

                if (currentUser.CountExecuting >= CountingExecuteBeforeSanctions + 1)
                {
                   
                    currentUser.CountWarning++;

                    if (currentUser.CountWarning == CountingExecuteBeforeTimeOut + 1)
                    {
                        TimeOutToUser();
                        _userActionInFilters.Remove(currentUser);
                    }
                    else
                    {
                        FilterResponce($"{this.WarningMessage} {CountingExecuteBeforeTimeOut - currentUser.CountWarning + 1}");
                    }
                }
            }          
        }

        private void FilterResponce(string message)
        {
            if (IsChatResponce)
            {
                TwitchBotGlobalObjects.Bot.SendMessage($"{(IsUserName ? this._message.Username : null)}{message}");
            }

            if (IsWhisResponce)
            {
                TwitchBotGlobalObjects.Bot.WhispMessage(this._message.Username, $"{(IsUserName ? this._message.Username : null)}{message}");
            }
        }

        private void TimeOutToUser()
        {
            TwitchBotGlobalObjects.Bot.Client.TimeoutUser(this._message.Channel,this._message.Username,this.UserTimeOutDuration,this.TimeOutMessage);
            FilterResponce($"{TimeOutMessage}");
        }

    }
}
