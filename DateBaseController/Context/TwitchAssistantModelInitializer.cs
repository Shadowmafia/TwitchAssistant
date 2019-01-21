using System;
using System.Data.Entity;
using DateBaseController.Models;
using SQLite.CodeFirst;

namespace DateBaseController.Context
{
    public class TwitchAssistantModelInitializer : SqliteDropCreateDatabaseAlways<TwitchAssistantContext>
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
                    Id = 1, Name = "help", Message = true, IsEnabled = true, Whisp = false, Description = "Get all enabled commands",Action = "Help"
                },
                new DefaultCommand()
                {
                    Id = 2, Name = "hello", Message = true, IsEnabled = false, Whisp = false, Description = "Say hello with you name",Action = "SayHello"
                },           
                new DefaultCommand()
                {
                    Id = 3, Name = "myinfo", Message = true, IsEnabled = true, Whisp = false, Description = "Check you info",Action = "MyInfo"
                },
                new DefaultCommand()
                {
                    Id = 4, Name = "userInfo", Message = true, IsEnabled = true, Whisp = false, Description = "(and username) Check user info",Action = "UserInfo"
                },
                new DefaultCommand()
                {
                    Id = 5, Name = "coins", Message = true, IsEnabled = true, Whisp = false, Description = $"Check you coins",Action = "Coins"
                },
                new DefaultCommand()
                {
                    Id = 6, Name = "uptime", Message = true, IsEnabled = true, Whisp = false, Description = "Get translation upTime",Action = "UpTime"
                },
            };

            PlayerCommand[] playerCommands = new PlayerCommand[]
            {
                new PlayerCommand()
                {
                    Id = 7, Name = "sr", Message = true, IsEnabled = true, Whisp = false, Description = "(and youtube link) Add song to chat playlist",Action = "SongRequest"
                },
                new PlayerCommand()
                {
                    Id = 8, Name = "srf", Message = true, IsEnabled = true, Whisp = false, Description = "(and youtube link) Add song to chat playlist with out queue",Action = "SongRequestFirst"
                },
                new PlayerCommand()
                {
                    Id = 9, Name = "ss", Message = true, IsEnabled = true, Whisp = false, Description = "Skip current song",Action = "SkipSong"
                },
                new PlayerCommand()
                {
                    Id = 10, Name = "sql", Message = true, IsEnabled = true, Whisp = false, Description = "Chat playlist queue length",Action = "SongQeueLeght"
                },
                new PlayerCommand()
                {
                    Id = 11, Name = "song", Message = true, IsEnabled = true, Whisp = false, Description = "Current song info",Action = "CurentSong"
                },
                new PlayerCommand()
                {
                    Id = 12, Name = "playlist", Message = false, IsEnabled = false, Whisp = false, Description = "Get streamer playlist",Action = "StreamerPlaylist"
                },
                new PlayerCommand()
                {
                    Id = 13, Name = "cPlaylist", Message = true, IsEnabled = true, Whisp = false, Description = "Get chat playlist",Action = "ChatPlaylist"
                },
            };
   

            context.DefaultCommands.AddRange(defaultCommands);
            context.PlayerCommands.AddRange(playerCommands);
            base.Seed(context);
        }


    }
}
