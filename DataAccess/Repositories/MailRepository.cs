using DataAccess.Classes;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MailRepository : IMailRepository
    {
        private readonly DbContextOptions<UsersDBContext> _options;

        public MailRepository(DbContextOptions<UsersDBContext> options)
        {
            _options = options;
        }

        public async Task<bool> ConfirmMail(EmailDTO email)
        {
            using (var context = new UsersDBContext(_options))
            { 
                var entity = await context.Emails.Where(x =>
                    x.UserId == email.UserId &&
                    x.ConfirmationMessage == email.ConfirmationMessage).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.IsConfirmed = true;
                    await context.SaveChangesAsync();
                }

                return entity != null;
            }
        }

        public async Task SaveMail(EmailDTO email)
        {
            using (var context = new UsersDBContext(_options))
            {
                await context.Emails.AddAsync(email);
                await context.SaveChangesAsync();
            }
        }
    }
}
