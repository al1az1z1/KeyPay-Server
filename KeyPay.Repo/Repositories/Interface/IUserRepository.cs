using KeyPay.Data.Models;
using KeyPay.Repo.Infrastructure;
using System.Threading.Tasks;

namespace KeyPay.Repo.Repositories.Interface
{
    public  interface IUserRepository : IRepository<User>
    {
        Task<bool> UserExist(string username);
    }
}
