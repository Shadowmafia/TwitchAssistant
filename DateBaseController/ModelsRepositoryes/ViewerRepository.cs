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
    public class ViewerRepository : BaseRepository<Viewer>
    {
        public ViewerRepository(TwitchAssistantContext context) : base(context)
        {
            _items = _db.Viewers.ToList();
        }

        public override Viewer Get(int id)
        {
            return _items.Find((viewer) => viewer.Id == id);
        }

        public override void Delete(int id)
        {
            Viewer viewer = _db.Viewers.Find(id);
            if (viewer != null)
            {
                _items.Remove(viewer);
                _db.Entry(viewer).State = EntityState.Deleted;
            }
        }

        public override void AddOrUpdate(IEnumerable<Viewer> items)
        {
            throw new NotImplementedException();
        }
    }
}
