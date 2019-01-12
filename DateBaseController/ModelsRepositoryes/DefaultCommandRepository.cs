using DateBaseController.Context;
using DateBaseController.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseController.ModelsRepositoryes
{
    public class DefaultCommandRepository : IRepository<DefaultCommand>
    {
        private TwitchAssistantContext _db;
        List<DefaultCommand> _defaultCommands;
        public DefaultCommandRepository(TwitchAssistantContext context)
        {
            this._db = context;
            _defaultCommands = _db.DefaultCommands.ToList();
        }
        public DefaultCommand Get(int id)
        {
           return _defaultCommands.Find((comand)=> comand.Id==id);
        }

        public IEnumerable<DefaultCommand> GetAll()
        {
            return _defaultCommands;
        }

        public void Add(DefaultCommand command)
        {
            _defaultCommands.Add(command);
            _db.Entry(command).State = EntityState.Added;
        }
        public void Update(DefaultCommand command)
        {
            _db.Entry(command).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            DefaultCommand command = _db.DefaultCommands.Find(id);
            if (command != null)
            {
                _defaultCommands.Remove(command);
                _db.Entry(command).State = EntityState.Deleted;
            }
        }

       
    }
}
