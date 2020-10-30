using KeyPay.Data.DatabaseContext;
using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using KeyPay.Services.Auth.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KeyPay.Repo.Repositories.Repo;
using KeyPay.Common.Helpers;

namespace KeyPay.Services.Auth.Services
{
    public class AuthService : IAuthService
    {


        public AuthService(IUnitOfWork<KeyPayDbContext> dbcontext)
        {
            _db = dbcontext;
        }


        private readonly IUnitOfWork<KeyPayDbContext> _db;

        public Task<User> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            Utilities.CreatePasswordhash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _db.UserRepository.InsertAsync(user);
            await _db.SaveAsync();

            return user;

        }
    }
}
