using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;

namespace KeyPay.Repo.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Normal Crud
        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> where);

        TEntity GetById(object id);

        IEnumerable<TEntity> GetAll();

        TEntity Get(Expression<Func<TEntity, bool>> where);

        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        #endregion /Normal Crud

        #region Async
        System.Threading.Tasks.Task InsertAsync(TEntity entity);

        System.Threading.Tasks.Task UpdateAsync(TEntity entity);

        System.Threading.Tasks.Task DeleteAsync(object id);

        System.Threading.Tasks.Task DeleteAsync(TEntity entity);

        System.Threading.Tasks.Task DeleteAsync(Expression<Func<TEntity, bool>> where);

        System.Threading.Tasks.Task<TEntity> GetByIdAsync(object id);

        System.Threading.Tasks.Task<IEnumerable<TEntity>>  GetAllAsync();

        System.Threading.Tasks.Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);

        System.Threading.Tasks.Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);

        #endregion /Async
    }
}
