using KeyPay.Data.DatabaseContext;
using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KeyPay.Repo.Repositories.Repo;
using KeyPay.Common.Helpers;
using KeyPay.Services.Site.Admin.Auth.Interface;

namespace KeyPay.Services.Site.Admin.Auth.Services
{
    public class AuthService : IAuthService
    {


        public AuthService(IUnitOfWork<KeyPayDbContext> dbcontext)
        {
            _db = dbcontext;
        }


        private readonly IUnitOfWork<KeyPayDbContext> _db;

        public async Task<User> Login(string username, string password)
        {
            var user = await _db.UserRepository.GetAsync(u => u.UserName.ToLower() == username.ToLower());
            if (user == null)
            {
                return null;
            }
            if (Utilities.PasswordVerified(password, user.PasswordHash, user.PasswordSalt))
            {
                return user;
            }
            return null;
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
