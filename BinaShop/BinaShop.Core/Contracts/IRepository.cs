using System;
using System.Linq;
using BinaShop.Core.Models;

namespace BinaShop.Core.Contracts
{
    public interface IRepository<D> where D : BaseEntity
    {
        IQueryable<D> Collection();
        void Commit();
        void Delete(string Id);
        D Find(string Id);
        void Insert(D d);
        void Update(D d);
    }
}