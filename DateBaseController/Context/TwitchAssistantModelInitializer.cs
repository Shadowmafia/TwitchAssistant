using System;
using System.Data.Entity;
using DataClasses.Enums;
using DateBaseController.Models;
using DateBaseController.Models.CommandsModels;
using SQLite.CodeFirst;

namespace DateBaseController.Context
{
    public class TwitchAssistantModelInitializer : SqliteCreateDatabaseIfNotExists<TwitchAssistantContext>
    {
        public TwitchAssistantModelInitializer(DbModelBuilder modelBuilder) : base(modelBuilder)
        {

        }
        public override void InitializeDatabase(TwitchAssistantContext context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(TwitchAssistantContext context)
        {        
            MessageTimer[] timers = new MessageTimer[]
            {
                new MessageTimer(){LastShow = DateTime.Now,Interval = new TimeSpan(0,5,0).Ticks,IsEnabled = true,Message = "enter !help for get all commands!"},
            };
            context.MessageTimers.AddRange(timers);

            DefaultCommand[] defaultCommands = new DefaultCommand[]
            {
                new DefaultCommand()
                {
                    Id = 1, Name = "help", Message = true, IsEnabled = true, Whisp = false,
                    Description = "Get all enabled commands", Action = "Help", UserLevel = TwitchRangs.Unfollower,
                    IsGlobalCooldown = false, GlobalCooldown = new TimeSpan(0, 0, 10).Ticks,
                    IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new DefaultCommand()
                {
                    Id = 2, Name = "hello", Message = true, IsEnabled = false, Whisp = false,
                    Description = "Say hello with you name", Action = "SayHello", UserLevel = TwitchRangs.Unfollower,
                    IsGlobalCooldown = false, GlobalCooldown = new TimeSpan(0, 0, 10).Ticks,
                    IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },           
                new DefaultCommand()
                {
                    Id = 3, Name = "myinfo", Message = true, IsEnabled = true, Whisp = false,
                    Description = "Check you info", Action = "MyInfo", UserLevel = TwitchRangs.Unfollower,
                    IsGlobalCooldown = false, GlobalCooldown = new TimeSpan(0, 0, 10).Ticks,
                    IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new DefaultCommand()
                {
                    Id = 4, Name = "userInfo", Message = true, IsEnabled = true, Whisp = false,
                    Description = "(and username) Check user info", Action = "UserInfo",
                    UserLevel = TwitchRangs.Moderator, IsGlobalCooldown = false,
                    GlobalCooldown = new TimeSpan(0, 0, 10).Ticks, IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new DefaultCommand()
                {
                    Id = 5, Name = "coins", Message = true, IsEnabled = true, Whisp = false,
                    Description = $"Check you coins", Action = "Coins", UserLevel = TwitchRangs.Unfollower,
                    IsGlobalCooldown = false, GlobalCooldown = new TimeSpan(0, 0, 10).Ticks,
                    IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new DefaultCommand()
                {
                    Id = 6, Name = "uptime", Message = true, IsEnabled = true, Whisp = false,
                    Description = "Get translation upTime", Action = "UpTime", UserLevel = TwitchRangs.Unfollower,
                    IsGlobalCooldown = false, GlobalCooldown = new TimeSpan(0, 0, 10).Ticks,
                    IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
            };

            PlayerCommand[] playerCommands = new PlayerCommand[]
            {
                new PlayerCommand()
                {
                    Id = 7, Name = "sr", Message = true, IsEnabled = true, Whisp = false,
                    Description = "(and youtube link) Add song to chat playlist", Action = "SongRequest",
                    UserLevel = TwitchRangs.Unfollower, IsGlobalCooldown = false,
                    GlobalCooldown = new TimeSpan(0, 0, 10).Ticks, IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new PlayerCommand()
                {
                    Id = 8, Name = "srf", Message = true, IsEnabled = true, Whisp = false,
                    Description = "(and youtube link) Add song to chat playlist with out queue",
                    Action = "SongRequestFirst", UserLevel = TwitchRangs.Unfollower, IsGlobalCooldown = false,
                    GlobalCooldown = new TimeSpan(0, 0, 10).Ticks, IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new PlayerCommand()
                {
                    Id = 9, Name = "ss", Message = true, IsEnabled = true, Whisp = false,
                    Description = "Skip current song", Action = "SkipSong", UserLevel = TwitchRangs.Unfollower,
                    IsGlobalCooldown = false, GlobalCooldown = new TimeSpan(0, 0, 10).Ticks,
                    IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new PlayerCommand()
                {
                    Id = 10, Name = "sql", Message = true, IsEnabled = true, Whisp = false,
                    Description = "Chat playlist queue length", Action = "SongQeueLeght",
                    UserLevel = TwitchRangs.Unfollower, IsGlobalCooldown = false,
                    GlobalCooldown = new TimeSpan(0, 0, 10).Ticks, IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new PlayerCommand()
                {
                    Id = 11, Name = "song", Message = true, IsEnabled = true, Whisp = false,
                    Description = "Current song info", Action = "CurentSong", UserLevel = TwitchRangs.Unfollower,
                    IsGlobalCooldown = false, GlobalCooldown = new TimeSpan(0, 0, 10).Ticks,
                    IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new PlayerCommand()
                {
                    Id = 12, Name = "playlist", Message = false, IsEnabled = false, Whisp = false,
                    Description = "Get streamer playlist", Action = "StreamerPlaylist",
                    UserLevel = TwitchRangs.Unfollower, IsGlobalCooldown = false,
                    GlobalCooldown = new TimeSpan(0, 0, 10).Ticks, IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
                new PlayerCommand()
                {
                    Id = 13, Name = "cPlaylist", Message = true, IsEnabled = true, Whisp = false,
                    Description = "Get chat playlist", Action = "ChatPlaylist", UserLevel = TwitchRangs.Unfollower,
                    IsGlobalCooldown = false, GlobalCooldown = new TimeSpan(0, 0, 10).Ticks,
                    IsGlobalErrorResponse = false, IsUserLevelErrorResponse = true, IsWhispErrors = true,
                    IsChatErrors = false
                },
            };
   

            context.DefaultCommands.AddRange(defaultCommands);
            context.PlayerCommands.AddRange(playerCommands);
            base.Seed(context);
        }


    }
}
