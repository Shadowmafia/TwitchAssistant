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
    public class MessageTimerRepository : IRepository<MessageTimer>
    {

        private TwitchAssistantContext _db;
        List<MessageTimer> _messageTimers;
        public MessageTimerRepository(TwitchAssistantContext context)
        {
            this._db = context;
            _messageTimers = _db.MessageTimers.ToList();
        }

        public MessageTimer Get(int id)
        {
           return _messageTimers.Find((timer)=> timer.Id==id);
        }

        public IEnumerable<MessageTimer> GetAll()
        {
            return _messageTimers;
        }

        public void Add(MessageTimer timer)
        {         
            _messageTimers.Add(timer);
            _db.Entry(timer).State = EntityState.Added;
        }

        public void Update(MessageTimer timer)
        {
            _db.Entry(timer).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            MessageTimer timer = _db.MessageTimers.Find(id);
            if (timer != null)
                _messageTimers.Remove(timer);
                _db.Entry(timer).State = EntityState.Deleted;
        }

     
    }
}
