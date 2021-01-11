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

        public UnitOfWork()
        {
            _db = new TContex();
        }

        protected readonly DbContext _db;

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

        private IPhotoRepository photoRepository;
        public IPhotoRepository PhotoRepository
        {
            get
            {
                if (photoRepository == null)
                {
                    photoRepository = new PhotoRepository(_db);
                }
                return photoRepository;
            }

        }

        #endregion /Private Repositories

        #region Save
        public bool Save()
        {
            if (_db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async System.Threading.Tasks.Task<bool> SaveAsync()
        {
            if (await _db.SaveChangesAsync() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion /Save



        #region Dispose
        //private bool disposed = false;
        public bool disposed { get; protected set; }



        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            if (_db != null)
        //            {
        //                _db.Dispose();
        //            }
        //        }
        //    }


        //    disposed = true;
        //}
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).

                if (_db != null)
                {
                    _db.Dispose();
                    //_db = null;
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

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
