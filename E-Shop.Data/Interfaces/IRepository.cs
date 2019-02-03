using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        List<TEntity> GetAll();
        TEntity FindById(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);            
    }
}
