using DateBaseController.Context;
using DateBaseController.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseController.ModelsRepositoryes
{
    public class MessageTimerRepository : BaseRepository<MessageTimer>
    {
        public MessageTimerRepository(TwitchAssistantContext context) : base(context)
        {
            _items = new BlockingCollection<MessageTimer>(new ConcurrentQueue<MessageTimer>(_db.MessageTimers));
        }

        public override void AddOrUpdate(IEnumerable<MessageTimer> items)
        {
            throw new NotImplementedException();
        }

        public override void Delete(int id)
        {
            MessageTimer timer = _db.MessageTimers.Find(id);
            if (timer != null)
                _items.TakeWhile(item=>item==timer);
            _db.Entry(timer).State = EntityState.Deleted;
        }

        public override MessageTimer Get(int id)
        {
            return _items.FirstOrDefault((timer) => timer.Id == id);
        }
    }
}
