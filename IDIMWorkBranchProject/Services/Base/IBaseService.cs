using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IDIMWorkBranchProject.Services.Base
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQueryableEntities();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(object id);
        Task<TEntity> DeleteAsync(TEntity entity);
        int GetCount(Expression<Func<TEntity, bool>> filter);
    }
}
