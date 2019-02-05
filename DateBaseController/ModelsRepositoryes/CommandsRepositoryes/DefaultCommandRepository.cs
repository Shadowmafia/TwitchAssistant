using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DateBaseController.Context;
using DateBaseController.Models.CommandsModels;

namespace DateBaseController.ModelsRepositoryes.CommandsRepositoryes
{
    public class DefaultCommandRepository : BaseRepository<DefaultCommand>
    {
        public DefaultCommandRepository(TwitchAssistantContext context) : base(context)
        {
            _items = new BlockingCollection<DefaultCommand>(new ConcurrentQueue<DefaultCommand>(_db.DefaultCommands));
        }
        public override DefaultCommand Get(int id)
        {
            return _items.FirstOrDefault((comand) => comand.Id == id);
        }

        public override void AddOrUpdate(IEnumerable<DefaultCommand> items)
        {
           _db.DefaultCommands.AddOrUpdate(items.ToArray());
        }

        public override void Delete(int id)
        {
            DefaultCommand command = _db.DefaultCommands.Find(id);
            if (command != null)
            {
                _items.TryTake(out command);
                _db.Entry(command).State = EntityState.Deleted;
            }
        }       
    }
}
