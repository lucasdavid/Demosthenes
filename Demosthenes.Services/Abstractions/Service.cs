using Demosthenes.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Demosthenes.Services
{
    public abstract class Service<TEntity> 
        where TEntity : class
    {
        #region Protected Fields
        protected readonly ApplicationDbContext db;
        #endregion Protected Fields

        #region Constructor
        protected Service(ApplicationDbContext _db)
        {
            db = _db;
        }
        #endregion Constructor

        public virtual ICollection<TEntity> All()
        {
            return db.Set<TEntity>().ToList();
        }
        public virtual async Task<ICollection<TEntity>> AllAsync()
        {
            return await db.Set<TEntity>().ToListAsync();
        }
        public virtual TEntity Find(params object[] keyValues)
        {
            return db.Set<TEntity>().Find(keyValues);
        }
        public virtual int Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            return db.SaveChanges();
        }
        public virtual int AddRange(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().AddRange(entities);
            return db.SaveChanges();
        }
        public virtual int Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }
        public virtual int Delete(object id)
        {
            return Delete(db.Set<TEntity>().Find(id));
        }
        public virtual int Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
            return db.SaveChanges();
        }
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await db.Set<TEntity>().FindAsync(keyValues);
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().AddRange(entities);
            return await db.SaveChangesAsync();
        }
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return await db.SaveChangesAsync();
        }
        public virtual async Task<int> DeleteAsync(params object[] keyValues)
        {
            var entity = await FindAsync(keyValues);
            return await DeleteAsync(entity);
        }
        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
            return await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
