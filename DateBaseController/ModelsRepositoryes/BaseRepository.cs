using DateBaseController.Context;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBaseController.ModelsRepositoryes
{
    public abstract class BaseRepository<T> where T : class
    {
        protected TwitchAssistantContext _db;
        protected BlockingCollection<T> _items;
        public  BaseRepository(TwitchAssistantContext context)
        {
            this._db = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _items;
        }
        public void Add(T item)
        {
            _items.Add(item);
            _db.Entry(item).State = EntityState.Added;
        }
        public void Update(T timer)
        {
            _db.Entry(timer).State = EntityState.Modified;
        }
        public abstract T Get(int id);

        public abstract void AddOrUpdate(IEnumerable<T> items);
        public abstract void Delete(int id);
    }
}
