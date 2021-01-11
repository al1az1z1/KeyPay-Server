using KeyPay.Common.Helpers;
using KeyPay.Data.DatabaseContext;
using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using KeyPay.Services.Site.Admin.Auth.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyPay.Services.Site.Admin.Auth.Services
{
    public class UserService : IUserService
    {
        public UserService(IUnitOfWork<KeyPayDbContext> dbContext)
        {
            _db = dbContext;
        }
        
        private readonly IUnitOfWork<KeyPayDbContext> _db;

        public async Task<User> GetUserForPassChange(System.Guid id , string password)
        {
            var user = await _db.UserRepository.GetByIdAsync(id);
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

        public async Task<bool> UpdateUserPass(User user, string newPassword)
        {
            byte[] passwordHash, passwordSalt;

            Utilities.CreatePasswordhash(newPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _db.UserRepository.Update(user);
            return await _db.SaveAsync();

        }
    }
}
