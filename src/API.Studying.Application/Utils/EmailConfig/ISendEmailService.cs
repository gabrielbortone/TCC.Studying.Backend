using System.Threading.Tasks;

namespace API.Studying.Application.Utils.EmailConfig
{
    public interface ISendEmailService
    {
        Task<bool> SendEmailAsync(string text, string subject, string to);
    }
}
