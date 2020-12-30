using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Services.Seed.Interface
{
    public interface ISeedService
    {
        System.Threading.Tasks.Task SeedUsersAsync();

       public void SeedUsers();
    }
}
