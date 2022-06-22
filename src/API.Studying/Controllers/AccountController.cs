using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Account;
using API.Studying.Application.Mediator;
using API.Studying.Application.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Studying.Controllers
{
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        public AccountController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync ([FromBody] AccountLoginCommand command)
        {
            try
            {
                var tokenDto = await _mediatorHandler.SendCommandDiferente<AccountLoginCommand>(command);
                if(tokenDto == null)
                {
                    throw new ArgumentException("Não foi possível gerar o token!");
                }
                return Ok(tokenDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }   
        }

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailCommand command)
        {
            try
            {
                var confirmEmail = await _mediatorHandler.SendCommandDiferente<ConfirmEmailCommand>(command);
                if (((ValidationResult)confirmEmail).IsValid)
                {
                    return Ok(confirmEmail);
                }

                return BadRequest(confirmEmail);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost("recoverPasswordPhase1")]
        public async Task<IActionResult> RecoverPasswordPhase1([FromBody] RecoverPasswordCommand command)
        {
            try
            {
                var recoverPassword = await _mediatorHandler.SendCommandDiferente<RecoverPasswordCommand>(command);

                if (((ValidationResult)recoverPassword).IsValid)
                {
                    return Ok(recoverPassword);
                }

                return BadRequest(recoverPassword);
                

            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost("recoverPasswordPhase2")]
        public async Task<IActionResult> RecoverPasswordPhase2([FromBody] RecoverPasswordConfirmedCommand command)
        {
            try
            {
                var recoverPassword = await _mediatorHandler.SendCommandDiferente<RecoverPasswordConfirmedCommand>(command);
                if (((ValidationResult)recoverPassword).IsValid)
                {
                    return Ok(recoverPassword);
                }

                return BadRequest(recoverPassword);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            try
            {
                var changePassword = await _mediatorHandler.SendCommandDiferente<ChangePasswordCommand>(command);
                if (((ValidationResult)changePassword).IsValid)
                {
                    return Ok(changePassword);
                }

                return BadRequest(changePassword);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }
    }
}