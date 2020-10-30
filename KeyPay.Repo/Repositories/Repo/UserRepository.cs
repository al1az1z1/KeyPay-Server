using Microsoft.EntityFrameworkCore;
using KeyPay.Data.Models;
using KeyPay.Repo.Repositories.Interface;
using KeyPay.Data.DatabaseContext;

namespace KeyPay.Repo.Repositories.Repo
{
    public class UserRepository : Infrastructure.Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            // i think it's better for DI
            //DbContext = dbContext;

            // یعمی مقدار  رو برابر با این دیتابیس کانتکس قرار بده 
            _db ??= (KeyPayDbContext)_db;


        }
        // i think it's better for DI
        //internal DbContext DbContext { get; }

        private readonly DbContext _db;


    }
}
