using BinaShop.Core.Contracts;
using BinaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaShop.DataAccess.SQL
{
    public class SQLRepository<D> : IRepository<D> where D : BaseEntity
    {
        internal DataContext context;
        internal DbSet<D> dbSet;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<D>();
        }
        public IQueryable<D> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var d = Find(Id);
            if (context.Entry(d).State == EntityState.Detached)
            {
                dbSet.Attach(d);
            }
            dbSet.Remove(d);
        }

        public D Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(D d)
        {
            dbSet.Add(d);
        }

        public void Update(D d)
        {
            dbSet.Attach(d);
            context.Entry(d).State = EntityState.Modified;
        }
    }
}
