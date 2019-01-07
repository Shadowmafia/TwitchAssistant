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

            // db.Users.Add(new User() {Username = "TestUser", LastCoinUpdate = DateTime.Now});

            MessageTimer[] timers = new MessageTimer[]
            {
                new MessageTimer(){LastShow = DateTime.Now,Interval = new TimeSpan(0,5,0).Ticks,IsEnabled = true,Message = "enter !help for get all commands!"},
            };
            context.MessageTimers.AddRange(timers);

            DefaultCommand[] userCommands = new DefaultCommand[]
            {
                new DefaultCommand()
                {
                    Id = 1, Name = "hello", Message = true, IsEnabled = false, Whisp = false, Description = "Say hello with you name"
                },
                new DefaultCommand()
                {
                    Id = 2, Name = "sr", Message = true, IsEnabled = true, Whisp = false, Description = "(and youtube link) Add song to chat playlist"
                },
                new DefaultCommand()
                {
                    Id = 3, Name = "srf", Message = true, IsEnabled = true, Whisp = false, Description = "(and youtube link) Add song to chat playlist with out queue"
                },
                new DefaultCommand()
                {
                    Id = 4, Name = "ss", Message = true, IsEnabled = true, Whisp = false, Description = "Skip current song"
                },
                new DefaultCommand()
                {
                    Id = 5, Name = "sql", Message = true, IsEnabled = true, Whisp = false, Description = "Chat playlist queue length"
                },
                new DefaultCommand()
                {
                    Id = 6, Name = "song", Message = true, IsEnabled = true, Whisp = false, Description = "Current song info"
                },
                new DefaultCommand()
                {
                    Id = 7, Name = "playlist", Message = false, IsEnabled = false, Whisp = false, Description = "Get streamer playlist"
                },
                new DefaultCommand()
                {
                    Id = 8, Name = "cPlaylist", Message = true, IsEnabled = true, Whisp = false, Description = "Get chat playlist"
                },
                new DefaultCommand()
                {
                    Id = 9, Name = "myinfo", Message = true, IsEnabled = true, Whisp = false, Description = "Check you info"
                },
                new DefaultCommand()
                {
                    Id = 10, Name = "userInfo", Message = true, IsEnabled = true, Whisp = false, Description = "(and username) Check user info"
                },
                new DefaultCommand()
                {
                    Id = 11, Name = "coins", Message = true, IsEnabled = true, Whisp = false, Description = $"Check you coins"
                },
                new DefaultCommand()
                {
                    Id = 12, Name = "uptime", Message = true, IsEnabled = true, Whisp = false, Description = "Get translation upTime"
                },
                new DefaultCommand()
                {
                    Id = 13, Name = "help", Message = true, IsEnabled = true, Whisp = false, Description = "Get all enabled chat commands"
                },

            };
            context.DefaultCommands.AddRange(userCommands);

            base.Seed(context);
        }


    }
}
