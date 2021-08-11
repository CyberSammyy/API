using BusinessLogic.Models;
using DataAccess.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IMailService
    {
        void SaveMailAddress(EmailDTO email);
        Task<bool> ConfirmMail(ConfirmationMessageModel model);
    }
}
