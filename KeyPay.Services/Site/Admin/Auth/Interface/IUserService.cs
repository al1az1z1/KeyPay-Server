using KeyPay.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyPay.Services.Site.Admin.Auth.Interface
{
    public interface IUserService
    {
        Task<User> GetUserForPassChange(Guid id, string password);

        Task<bool> UpdateUserPass(User user, string newPassword);
    }
}
