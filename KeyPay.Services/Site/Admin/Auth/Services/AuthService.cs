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
using System.Linq;

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
            // برای اینکه میخواستیم عکس هم هنگام لاگین بفرستیم چون یک کالکشن هست باید از  تابع دیگری استفاده میکردیم که به نظرم درست نیست 
            //چون الان کلی دیتا رو میگیریم بعدش میایم میگیم سینگل رو بده
            //var user = await _db.UserRepository.GetAsync(u => u.UserName.ToLower() == username.ToLower());

            var users = await _db.UserRepository.GetManyAsync(u => u.UserName.ToLower() == username.ToLower(),null, "Photoes");

            var user = users.SingleOrDefault();

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

        public async Task<User> Register(User user, Photo photo, string password)
        {
            byte[] passwordHash, passwordSalt;

            Utilities.CreatePasswordhash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _db.UserRepository.InsertAsync(user);
            await _db.PhotoRepository.InsertAsync(photo);
            await _db.SaveAsync();

            return user;

        }


        



    }
}
