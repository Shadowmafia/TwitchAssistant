using DateBaseController.Context;
using DateBaseController.Models.CommandsModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseController.ModelsRepositoryes.CommandsRepositoryes
{
    public class CustomCommandRepository : BaseRepository<CustomCommand>
    {
        public CustomCommandRepository(TwitchAssistantContext context) : base(context)
        {
           _items = new BlockingCollection<CustomCommand>(new ConcurrentQueue<CustomCommand>(_db.CustomCommands));
        }
        public override void AddOrUpdate(IEnumerable<CustomCommand> items)
        {
            _db.CustomCommands.AddOrUpdate(items.ToArray());
        }
        public override void Delete(int id)
        {
            CustomCommand command = _db.CustomCommands.Find(id);
            if (command != null)
            {
                _items.TakeWhile(item => item == command);
                _db.Entry(command).State = EntityState.Deleted;
            }
        }
        public override CustomCommand Get(int id)
        {
            return _items.FirstOrDefault((comand) => comand.Id == id);
        }
    }
}
