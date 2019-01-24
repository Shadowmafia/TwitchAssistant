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
    public class ViewerRepository : BaseRepository<Viewer>
    {
        public ViewerRepository(TwitchAssistantContext context) : base(context)
        {
            _items = new BlockingCollection<Viewer>(new ConcurrentQueue<Viewer>(_db.Viewers));
        }

        public override Viewer Get(int id)
        {
            return _items.FirstOrDefault((viewer) => viewer.Id == id);
        }

        public override void Delete(int id)
        {
            Viewer viewer = _db.Viewers.FirstOrDefault(user=>user.Id==id);
            if (viewer != null)
            {
                _items.TakeWhile(item=>item==viewer);
                _db.Entry(viewer).State = EntityState.Deleted;
            }
        }

        public override void AddOrUpdate(IEnumerable<Viewer> items)
        {
            throw new NotImplementedException();
        }
        public void DeletAll()
        {
            while (_items.Count > 0)
            {
                var obj = _items.Take();
            }
            foreach (var item in _db.Viewers)
            {
                _db.Viewers.Remove(item);
            }
        }
    }
}
