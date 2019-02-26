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
            dbSet = context.Set<TEntity>();
        }

        protected DbContext context;
        protected DbSet<TEntity> dbSet;

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
           TEntity entity = dbSet.Find(id);
            try
            {
                dbSet.Remove(entity);
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
            return dbSet.Find(id);
        }

        public List<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public void Update(TEntity entity)
        {
            if (dbSet.Contains(entity))
            {
                dbSet.Update(entity);
            }
            else
            {
                dbSet.Add(entity);
                context.SaveChanges();
            }
        }
    }
}
