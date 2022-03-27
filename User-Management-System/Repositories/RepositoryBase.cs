using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        internal readonly bootcampdb2Context Db;
        private readonly DbSet<T> _dbSet;

        internal RepositoryBase(bootcampdb2Context context)
        {
            Db = context;
            _dbSet = Db.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            Db.SaveChanges();
        }
        public virtual T Get(Expression<Func<T, bool>> where)
        {
            DetachAllEntities();
            return _dbSet.Where(where).FirstOrDefault();
        }
        public virtual T Get(int id)
        {
            DetachAllEntities();
            return _dbSet.Find(id);
        }
        public virtual T Get(decimal id)
        {
            DetachAllEntities();
            return _dbSet.Find(id);
        }
        public virtual T Get(float id)
        {
            DetachAllEntities();
            return _dbSet.Find(id);
        }
        public virtual T Get(string id)
        {
            DetachAllEntities();
            return _dbSet.Find(id);
        }
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
            Db.Entry(entity).State = EntityState.Modified;
            Db.SaveChanges();
        }
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            Db.SaveChanges();
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }
        public virtual IQueryable<T> AsQueryable() => _dbSet.AsNoTracking().AsQueryable();
        public virtual IEnumerable<T> GetAll() => _dbSet.ToList();
        public virtual void Commit()
        {
            DetachAllEntities();
        }
        public virtual void Save() => Db.SaveChanges();
        public virtual void Rollback()
        {
            DetachAllEntities();
        }
        public virtual void Dispose()
        {
            Db.Dispose();
        }
        public void DetachAllEntities()
        {
            IEnumerable<EntityEntry> changedEntriesCopy = Db.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified
                        || x.State == EntityState.Added
                        || x.State == EntityState.Deleted);
            foreach (var entity in changedEntriesCopy)
            {
                entity.State = EntityState.Detached;
            }
        }
        public int GetMaxPK(string pkPropertyName)
        {
            // TODO: add argument checks
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression body = Expression.Property(parameter, pkPropertyName);
            Expression<Func<T, int>> lambda = Expression.Lambda<Func<T, int>>(body, parameter);
            int result = _dbSet.Max(lambda);
            return result;
        }
    }
}