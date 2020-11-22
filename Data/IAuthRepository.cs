using System.Threading.Tasks;
using Models;
using Services;

namespace Data
{
    public interface IAuthRepository
    {
        //Register returns userId as integer
        Task<ServiceResponse<int>> Register(User user, string password);
        //Login returns token as string
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}