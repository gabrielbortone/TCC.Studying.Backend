using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace API.Studying.Application.Utils.EmailConfig
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _configuration;
        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(string text, string subject, string to)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("studyingapp0@gmail.com", "Studying App"),
                Subject = subject,
                HtmlContent = text
            };
            msg.AddTo(new EmailAddress(to, "Caro usuário(a)"));
            try
            {
                var response = await client.SendEmailAsync(msg);
                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
