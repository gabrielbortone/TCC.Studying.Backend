using API.Studying.Application.Utils.EmailConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Studying.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ISendEmailService _sendEmailService;

        public TestController(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        [HttpGet("sendEmailTest")]
        public async Task<IActionResult> SendEmail()
        {
            var content = @"<a href=""http://localhost:3000"" target=""_blank""><img src = ""https://studyingstorage.blob.core.windows.net/config/Logo_horizontal_branco.png"" alt = ""Logo Studying"" width = ""220"" title = ""Logo Studying""></a>";
            await _sendEmailService.SendEmailAsync(content, "Test","newgabrielbortone@gmail.com");
            return Ok();
        }
    }
}
