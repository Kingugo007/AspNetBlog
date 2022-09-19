using Medik.Core.ViewModel;
using Medik.Domain.Model;
using System.Threading.Tasks;

namespace Medik.Core.Interfaces
{
    public interface IAuthServices
    {
        Task<bool> IsExist(string email);
        Task<RegisterViewModel> Register(RegisterViewModel newUser);
        Task<User> Login(LoginViewModel userInfo);
    }
}