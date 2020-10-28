using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.Repositories.Repo
{
    public class UserRepository : Infrastructure.Repository<Models.User>, Repositories.Interface.IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            // i think it's better for DI
            //DbContext = dbContext;

            // یعمی مقدار  رو برابر با این دیتابیس کانتکس قرار بده 
            _db ??= (DatabaseContext.KeyPayDbContext)_db;

        }
        // i think it's better for DI
        //internal DbContext DbContext { get; }

        private readonly DbContext _db;


    }
}
