using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DateBaseController.Context;
using DateBaseController.Models.CommandsModels;

namespace DateBaseController.ModelsRepositoryes.CommandsRepositoryes
{
    public class PlayerCommandRepository : BaseRepository<PlayerCommand>
    {
        public PlayerCommandRepository(TwitchAssistantContext context) : base(context)
        {
            _items = new BlockingCollection<PlayerCommand>(new ConcurrentQueue<PlayerCommand>(_db.PlayerCommands));
        }

        public override void AddOrUpdate(IEnumerable<PlayerCommand> items)
        {
            _db.PlayerCommands.AddOrUpdate(items.ToArray());
        }

        public override void Delete(int id)
        {
            PlayerCommand command = _db.PlayerCommands.Find(id);
            if (command != null)
            {
                _items.TryTake(out command);
                _db.Entry(command).State = EntityState.Deleted;
            }
        }

        public override PlayerCommand Get(int id)
        {
            return _items.FirstOrDefault(command => command.Id == id);
        }

    }
}
