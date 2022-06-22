using API.Studying.Application.Commands.Suport;
using API.Studying.Application.Mediator;
using API.Studying.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Studying.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuportController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        public SuportController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateSuportRequestCommand suportRequestCommand)
        {
            try
            {
                var validationResult = await _mediatorHandler.SendCommand<CreateSuportRequestCommand>(suportRequestCommand);
                if ((validationResult).IsValid)
                {
                    return Ok(validationResult);
                }
                return BadRequest(validationResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }
    }
}
