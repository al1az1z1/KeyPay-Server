using KeyPay.Data.DatabaseContext;
using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;



namespace KeyPay.Services.Seed.Services
{
   public class SeedService : Seed.Interface.ISeedService
    {
        public SeedService(IUnitOfWork<KeyPayDbContext> dbcontext)
        {
            _db = dbcontext;
        }


        private readonly IUnitOfWork<KeyPayDbContext> _db;

        public async System.Threading.Tasks.Task SeedUsersAsync()
        {
            var userData = System.IO.File.ReadAllText("File/UserSeedData.json");

            var users = JsonConvert.DeserializeObject<IList<User>>(userData);

            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;

                Common.Helpers.Utilities.CreatePasswordhash("555555", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                user.UserName = user.UserName.ToLower();

               await _db.UserRepository.InsertAsync(user);

            }

            await _db.SaveAsync();
        }

        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("File/UserSeedData.json");

            var users = JsonConvert.DeserializeObject<IList<User>>(userData);

            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;

                Common.Helpers.Utilities.CreatePasswordhash("555555", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                user.UserName = user.UserName.ToLower();

                 _db.UserRepository.Insert(user);

            }

            _db.Save();
        }
    }
}
