using Microsoft.EntityFrameworkCore;
using KeyPay.Data.Models;
using KeyPay.Repo.Repositories.Interface;
using KeyPay.Data.DatabaseContext;
using System.Threading.Tasks;

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

        // Private function for Private Repository
        public async Task<bool> UserExist(string username)
        {
            if (await GetAsync(current => current.UserName.ToLower() == username.ToLower()) !=null )
            {
                return true;
            }
            return false;
        }
    }
}
