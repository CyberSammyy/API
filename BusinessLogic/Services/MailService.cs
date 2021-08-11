using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataAccess.Interfaces;
using DataAccess.Models;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly IMailRepository _mailRepository;

        private readonly IMapper _mapper;

        public MailService(IMailRepository mailRepository,
            IMapper mapper)
        {
            _mailRepository = mailRepository;
            _mapper = mapper;
        }

        public async Task<bool> ConfirmMail(ConfirmationMessageModel model)
        {
            return await _mailRepository.ConfirmMail(_mapper.Map<EmailDTO>(model));
        }

        public void SaveMailAddress(EmailDTO email)
        {
            _mailRepository.SaveMail(email);
        }
    }
}
