using KeyPay.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyPay.Services.Auth.Interface
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);
    }
}
