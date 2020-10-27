using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Data.DatabaseContext
{
    class KeyPayDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=KeyPay;Integrated Security=true;MultipleActiveResultSets=true;User ID=sa;Password=124000;");
        }
    }
}
