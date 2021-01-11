using KeyPay.Data.DatabaseContext;
using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using KeyPay.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyPay.Repo.Repositories.Repo
{
   public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DbContext dbContext) : base(dbContext)
        {
           
           _db =  _db ?? (KeyPayDbContext)_db;


        }

        private readonly DbContext _db;
    }
}
