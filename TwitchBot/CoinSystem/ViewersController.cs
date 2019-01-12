using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using DateBaseController;
using DateBaseController.Models;
using TwitchLib.Api.V5.Models.Channels;
using TwitchLib.Client.Events;

namespace TwitchBot.CoinSystem
{

    public class ViewersController
    {
        private readonly string _channelId;
        private readonly Timer _timer;

        public ViewersController()
        {
            _channelId = TwitchApiController.GetChanel().Result.Id;
            SyncUsersTwitchRang();

        }

        public void SyncUsersTwitchRang()
        {
            List<ChannelFollow> followers = TwitchApiController.Api.V5.Channels.GetAllFollowersAsync(_channelId).Result;
            TwitchBotGlobalObjects.ChanelData.Followers.Clear();
            foreach (var follower in followers)
            {
                TwitchBotGlobalObjects.ChanelData.Followers.Add(follower);
            }

            //Unfollow exist user in  date base  with was unfollow.
            var usersToUnfollow = AssistantDb.Instance.Viewers.GetAll().Where(new Func<Viewer, bool>((user) =>
            {
                return user.IsFollower && 
                       !TwitchBotGlobalObjects.ChanelData.Followers.Any(follow => follow.User.Id == user.Id.ToString());
            }));


            foreach (var unfollower in usersToUnfollow)
            {
                unfollower.IsFollower = false;
            }


            //Add new  followers  witch not  exist in the  date base.
            var newUsersFollowers = TwitchBotGlobalObjects.ChanelData.Followers.Where(
                new Func<ChannelFollow, bool>(
                    (follow) =>
                    {
                        return !AssistantDb.Instance.Viewers.GetAll().Any(user => user.Id.ToString() == follow.User.Id);
                    }
                )
            );

            foreach (var newUsers in newUsersFollowers)
            {
                int viewerId = int.Parse(newUsers.User.Id);
                Viewer joinedViewer = new Viewer() { Id = viewerId, Username = newUsers.User.Name, IsFollower = true };
                AssistantDb.Instance.Viewers.Add(joinedViewer);              
            }

            //Update exist users follow  status.
            var existUsersToFollower = AssistantDb.Instance.Viewers.GetAll().Where(
                new Func<Viewer, bool>(
                (user) =>
                {
                    return !user.IsFollower &&
                           TwitchBotGlobalObjects.ChanelData.Followers.Any(follow => follow.User.Id == user.Id.ToString());
                })
            );

            foreach (var userToFollower in existUsersToFollower)
            {
                userToFollower.IsFollower = true;
            }

            AssistantDb.Instance.SaveChanges();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
        
        internal void OnUserJoined(object sender, OnUserJoinedArgs e)
        {

            if (!TwitchBotGlobalObjects.ChanelData.Watchers.Any(user => user.Username == e.Username))
            {
                try
                {
                    Viewer joinedUser = AssistantDb.Instance.Viewers.GetAll().FirstOrDefault(viewer => viewer.Username == e.Username);

                    if (joinedUser == null)
                    {
                        var user = TwitchApiController.Api.V5.Users.GetUserByNameAsync(e.Username).Result;
                        int viewerId = int.Parse(user.Matches[0].Id);
                        joinedUser = new Viewer() { Id = viewerId, Username = e.Username, Rang = new Rang() { RangSets = new List<RangSet>() { new RangSet() } } };
                    
                        AssistantDb.Instance.Viewers.Add(joinedUser);
                        joinedUser = AssistantDb.Instance.Viewers.Get(joinedUser.Id);

                    }
                    joinedUser.LastCoinUpdate = DateTime.Now;
                    joinedUser.LastConnectToStream=DateTime.Now;
                    AssistantDb.Instance.SaveChanges();
                    TwitchBotGlobalObjects.ChanelData.Watchers.Add(joinedUser);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public void OnUserLeft(object sender, OnUserLeftArgs onUserLeftArgs)
        {
            var LeftUser= TwitchBotGlobalObjects.ChanelData.Watchers.First(user => user.Username == onUserLeftArgs.Username);
            TwitchBotGlobalObjects.ChanelData.Watchers.Remove(LeftUser);
            LeftUser.WatchingTime= LeftUser.WatchingTime.Add(DateTime.Now - LeftUser.LastConnectToStream);
            LeftUser.LastConnectToStream = DateTime.Now;
            AssistantDb.Instance.SaveChanges(); 
        }
    }
}
