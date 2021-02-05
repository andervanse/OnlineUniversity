using System.Threading.Tasks;

namespace OnlineUniversity.Infrastructure.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Email email);
    }
}
