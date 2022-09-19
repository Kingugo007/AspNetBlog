using Medik.Core.Utilities;
using Medik.Core.ViewModel;
using Medik.Domain.Model;
using System.Threading.Tasks;

namespace Medik.Infrastructure.AccountRepository
{
    public interface IAccountRepository
    {
        Task<Response> Register(RegisterViewModel userInfo);
        Task<User> Login(LoginViewModel userInfo);
    }
} 