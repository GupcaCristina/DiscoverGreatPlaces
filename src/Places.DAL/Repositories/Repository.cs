using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Places.DAL.Interfaces;
using System;


namespace Places.DAL.Repositories
{
    public class IRepository<TEntity> : Interfaces.IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbset;

        public IRepository(DbContext context)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
        }
        public void Delete(int id)
        {
            var entityToDelete = _dbset.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
        }
        public void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;

        }
        public  IQueryable<TEntity> Get() 
        {
            return _dbset;
        }

        public TEntity GetById(int id)
        {
            return _dbset.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public void AddRange(IList<TEntity> values)
        {
            _dbset.AddRange(values);     
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
