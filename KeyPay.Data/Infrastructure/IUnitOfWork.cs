
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Infrastructure
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        Repositories.Interface.IUserRepository IUserRepository {get;}

        void Save();

        System.Threading.Tasks.Task<int> SaveAsync();

    }
}
