using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateBaseController.Context;
using DateBaseController.Models;

namespace DateBaseController.ModelsRepositoryes
{
    public class PlayerCommandRepository : IRepository<PlayerCommand>
    {
        private TwitchAssistantContext _db;
        List<PlayerCommand> _playerCommands;

        public PlayerCommandRepository(TwitchAssistantContext context)
        {
            this._db = context;
            _playerCommands = _db.PlayerCommands.ToList();
        }
        public PlayerCommand Get(int id)
        {
            return _playerCommands.Find((command) => command.Id == id);
        }

        public IEnumerable<PlayerCommand> GetAll()
        {
            return _playerCommands;
        }

        public void Add(PlayerCommand command)
        {
            _playerCommands.Add(command);
            _db.Entry(command).State = EntityState.Added;
        }

        public void Update(PlayerCommand command)
        {
            _db.Entry(command).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            PlayerCommand command = _db.PlayerCommands.Find(id);
            if (command != null)
            {
                _playerCommands.Remove(command);
                _db.Entry(command).State = EntityState.Deleted;
            }
        }      
    }
}
