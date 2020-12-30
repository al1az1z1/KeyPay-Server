using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KeyPay.Repo.Infrastructure
{
    public abstract class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        public Repository(DbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        private readonly DbContext _db;
        private readonly DbSet<TEntity> _dbSet;



        #region Sync Crud

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("there is no entity");
            }
            _dbSet.Update(entity);
        }

        public void Delete(object id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                throw new ArgumentException("the entity is not valid");
            }

            _dbSet.Remove(entity);

        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> entities = _dbSet.Where(where).AsEnumerable();
            foreach (TEntity item in entities)
            {
                _dbSet.Remove(item);

            }
        }

        public TEntity GetById(object id)
        {
            var result = _dbSet.Find(id);
            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var result = _dbSet.ToList();
            return result;
        }

        //should use in XXXxxxrepo
        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            var result = _dbSet.Where(where).FirstOrDefault();
            return result;
        }


        // for retrieve for example  users accompany with photoes and bankcards
        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>>
            filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeEntity = ""
            )
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var item in includeEntity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);

            }
            if (orderBy != null)
            {
               return orderBy(query).ToList();
            }

            else
            {
                return query.ToList();
            }
        }

        #endregion / sync Crud


        #region Async Crud
        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("there is no entity");
            }

            await Task.Run(() =>
            {
                _dbSet.Update(entity);
            });

        }
        public async Task DeleteAsync(object id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                throw new ArgumentException("there is no entity");
            }

            await Task.Run(() =>
            {
                _dbSet.Remove(entity);
            });
        }
        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _dbSet.Remove(entity);
            });
        }
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> where)
        {

            await Task.Run(() =>
            {
                IEnumerable<TEntity> entities = _dbSet.Where(where).AsEnumerable();
                foreach (TEntity item in entities)
                {
                    _dbSet.Remove(item);

                }
            });
        }
        public async Task<TEntity> GetByIdAsync(object id)
        {

            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbSet.Where(where).FirstOrDefaultAsync();
        }


        // for retrieve for example  users accompany with photoes and bankcards
        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity,
            bool>> filter = null , Func<IQueryable<TEntity> , IOrderedQueryable<TEntity>> orderBy = null,
            string includeEntity = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);    
            }

            foreach (var item in includeEntity.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            else
            {
                return await query.ToListAsync();
            }
            
        }



        #endregion /Async Crud

        #region /dispose


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_db != null)
                    {
                        _db.Dispose();
                    }
                }
            }


            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        ~Repository()
        {
            Dispose(false);
        }

        #endregion /dispose
    }
}
