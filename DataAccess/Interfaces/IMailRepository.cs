using DataAccess.Models;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IMailRepository
    {
        Task SaveMail(EmailDTO email);
        Task<bool> ConfirmMail(EmailDTO email);
    }
}
