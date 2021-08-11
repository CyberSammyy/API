using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IMailExchangerService
    {
        Task SendMessage(string destMail, string messageSubject, string messageBody);
    }
}
