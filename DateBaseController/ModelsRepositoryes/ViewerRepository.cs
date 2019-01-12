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
    public class ViewerRepository : IRepository<Viewer>
    {

        private TwitchAssistantContext _db;
        List<Viewer> _viewers;
        public ViewerRepository(TwitchAssistantContext context)
        {
            this._db = context;
            _viewers = _db.Viewers.ToList();
        }

        public Viewer Get(int id)
        {
           return _viewers.Find((viewer)=>viewer.Id==id);
        }
        public IEnumerable<Viewer> GetAll()
        {
            return _viewers;
        }
        public void Add(Viewer viewer)
        {
            _viewers.Add(viewer);
            _db.Entry(viewer).State = EntityState.Added;
        }
        public void Update(Viewer viewer)
        {
            _db.Entry(viewer).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Viewer viewer = _db.Viewers.Find(id);
            if (viewer != null)
            {
                _viewers.Remove(viewer);
                _db.Entry(viewer).State = EntityState.Deleted;
            }
        }
        
    }
}
