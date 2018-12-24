using BinaShop.Core.Contracts;
using BinaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.DataAccess.InMemory
{
    public class InMemoryRepository<D> : IRepository<D> where D : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<D> items;
        string className;
        public InMemoryRepository()
        {
            className = typeof(D).Name;
            items = cache[className] as List<D>;
            if (items == null)
            {
                items = new List<D>();
            }

        }
        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(D d)
        {
            items.Add(d);
        }

        public void Update(D d)
        {
            D dToUpdate = items.Find(i => i.Id == d.Id);
            if (dToUpdate != null)
            {
                dToUpdate = d;
            }
            else
            {
                throw new Exception(className + "לא נמצא");
            }
        }

        public D Find(string Id)
        {
            D d = items.Find(i => i.Id == Id);
            if (d != null)
            {
                return d;
            }
            else
            {
                throw new Exception(className + "לא נמצא");
            }
        }

        public IQueryable<D> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            D dToDelete = items.Find(i => i.Id == Id);
            if (dToDelete != null)
            {
                items.Remove(dToDelete);
            }
            else
            {
                throw new Exception(className + "לא נמצא");
            }
        }
    }
}
