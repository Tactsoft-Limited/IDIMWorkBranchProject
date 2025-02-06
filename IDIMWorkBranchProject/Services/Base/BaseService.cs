using BGB.Data.Entities.Base;

using IDIMWorkBranchProject.Data.Database;
using IDIMWorkBranchProject.Extentions.Session;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Base
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
	{
        protected readonly IDIMDBEntities _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseService(IDIMDBEntities context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQueryableEntities()
        {
            return _dbSet.AsQueryable();
        }

        // Create
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
			entity.CreatedUser =  UserExtention.GetUserId();
			entity.CreatedDateTime = DateTime.Now;
			_dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Update
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.UpdatedUser = UserExtention.GetUserId();
            entity.UpdatedDateTime = DateTime.Now;
            entity.UpdateNo += 1;
            _context.Entry(entity).State = EntityState.Modified;
			// Exclude CreatedUser and CreatedDateTime from being modified
			_context.Entry(entity).Property(e => e.CreatedUser).IsModified = false;
			_context.Entry(entity).Property(e => e.CreatedDateTime).IsModified = false;
			await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }

}