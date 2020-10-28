using KeyPay.Repo.Repositories.Interface;
using KeyPay.Repo.Repositories.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Repo.Infrastructure
{
    public class UnitOfWork<TContex> : IUnitOfWork<TContex> where TContex : DbContext, new()
    {
        #region ctor
        protected readonly DbContext _db;
        public UnitOfWork()
        {
            _db = new TContex();
        }

        #endregion /ctor

        #region Private Repositories

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_db);
                }
                return userRepository;
            }

        }

        #endregion /Private Repositories

        #region Save
        public void Save()
        {
            _db.SaveChanges();
        }

        public System.Threading.Tasks.Task<int> SaveAsync()
        {
            return _db.SaveChangesAsync();
        }
        #endregion /Save

     

        #region Dispose
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

        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion /Dispose

    }

}
