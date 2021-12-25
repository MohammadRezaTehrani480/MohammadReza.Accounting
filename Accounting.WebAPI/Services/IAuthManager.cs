using Accounting.Shared.ViewModels.AccountViewModels;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken(LoginUserDTO userDTO);
    }
}
