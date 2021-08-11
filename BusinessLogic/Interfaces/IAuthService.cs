using BusinessLogic.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<ValidationResult> Login(AuthenticationModel authenticationModel);
        Task<bool> RegisterUser(User userToRegister);
        Task<ConfirmationResult> ConfirmEmail(string message);
    }
}
