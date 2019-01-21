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
            _items = _db.DefaultCommands.ToList();
        }
        public override DefaultCommand Get(int id)
        {
            return _items.Find((comand) => comand.Id == id);
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
                _items.Remove(command);
                _db.Entry(command).State = EntityState.Deleted;
            }
        }       
    }
}
