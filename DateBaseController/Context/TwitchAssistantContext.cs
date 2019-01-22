using System.Data.Entity;
using DateBaseController.Models;
using DateBaseController.Models.CommandsModels;

namespace DateBaseController.Context
{
    public partial class TwitchAssistantContext : DbContext
    {   
        public TwitchAssistantContext(): base("DefaultConnection"){}

        public virtual DbSet<Viewer> Viewers { get; set; } 
        public virtual DbSet<MessageTimer> MessageTimers { get; set; }
        public virtual DbSet<DefaultCommand> DefaultCommands { get; set; }
        public virtual DbSet<PlayerCommand> PlayerCommands { get; set; }
        public virtual DbSet<CustomCommand> CustomCommands { get; set; }
        // public virtual DbSet<Rang> Rangs { get; set; }
        // public virtual DbSet<RangSet> RangSets { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {      
            var initializer = new TwitchAssistantModelInitializer(modelBuilder);
            Database.SetInitializer(initializer);
        }
    }
}
