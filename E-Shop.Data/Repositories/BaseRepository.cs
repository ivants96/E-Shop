using E_Shop.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public BaseRepository()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .Options);
            dbset = context.Set<TEntity>();
        }

        protected DbContext context;
        protected DbSet<TEntity> dbset;

        public void Add(TEntity entity)
        {
            dbset.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
           TEntity entity = dbset.Find(id);
            try
            {
                dbset.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception)
            {
                context.Entry(entity).State = EntityState.Unchanged;
                throw;
            }
        }

        public TEntity FindById(int id)
        {
            return dbset.Find(id);
        }

        public List<TEntity> GetAll()
        {
            return dbset.ToList();
        }

        public void Update(TEntity entity)
        {
            if (dbset.Contains(entity))
            {
                dbset.Update(entity);
            }
            else
            {
                dbset.Add(entity);
                context.SaveChanges();
            }
        }
    }
}
