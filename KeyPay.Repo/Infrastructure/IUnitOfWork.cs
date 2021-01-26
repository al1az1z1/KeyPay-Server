using KeyPay.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyPay.Repo.Infrastructure
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IUserRepository UserRepository { get; }

        IPhotoRepository PhotoRepository { get; }

        ISettingRepository SettingRepository { get; }


        bool Save();

        Task<bool> SaveAsync();

    }
}
