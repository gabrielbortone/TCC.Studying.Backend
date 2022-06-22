using API.Studying.Application.Commands.Admin;
using API.Studying.Application.Mediator;
using API.Studying.Application.Services;
using API.Studying.Application.Utils.Jwt;
using API.Studying.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;

namespace API.Studying.Controllers
{
    [Authorize("Bearer")]
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IStudentRepository _studentRepository;

        public AdminController(IMediatorHandler mediatorHandler, 
                               IStudentRepository studentRepository)
        {
            _mediatorHandler = mediatorHandler;
            _studentRepository = studentRepository;
        }
        private bool IsAdminCheck()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(accessToken))
            {
                return false;
            }

            Guid studentId = Guid.Parse(TokenService.GetStudentId(accessToken.ToString().Replace("Bearer ","")));
            var student = _studentRepository.GetById(studentId);
            if(student == null)
            {
                return false;
            }
            else
            {
                return student.IsAdmin;
            }
        }

        [HttpPut("blockStudent")]
        public async Task<IActionResult> PutAsync([FromBody] AdminBlockStudentCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var validationResult = await _mediatorHandler.SendCommand<AdminBlockStudentCommand>(command);
                
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

        [HttpPut("turnStudentIntoAdmin")]
        public async Task<IActionResult> TurnStudentIntoAdminAsync([FromBody] AdminTurnStudentIntoAdminCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var validationResult = await _mediatorHandler.SendCommand<AdminTurnStudentIntoAdminCommand>(command);

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

        [HttpPost("createTopic")]
        public async Task<IActionResult> CreateTopicAsync([FromBody] AdminCreateTopicCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var validationResult = await _mediatorHandler.SendCommand<AdminCreateTopicCommand>(command);
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

        [HttpDelete("document")]
        public async Task<IActionResult> DeleteAsync([FromBody] AdminRemoveDocumentCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var validationResult = await _mediatorHandler.SendCommand<AdminRemoveDocumentCommand>(command);
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

        [HttpDelete("student")]
        public async Task<IActionResult> DeleteAsync([FromBody] AdminRemoveStudentCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var validationResult = await _mediatorHandler.SendCommand<AdminRemoveStudentCommand>(command);
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

        [HttpDelete("topic")]
        public async Task<IActionResult> DeleteAsync([FromBody] AdminRemoveTopicCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var validationResult = await _mediatorHandler.SendCommand<AdminRemoveTopicCommand>(command);
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

        [HttpDelete("video")]
        public async Task<IActionResult> DeleteAsync([FromBody] AdminRemoveVideoCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var validationResult = await _mediatorHandler.SendCommand<AdminRemoveVideoCommand>(command);
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

        [HttpPut("document")]
        public async Task<IActionResult> PutAsync([FromBody] AdminUpdateDocumentCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }


                var validationResult = await _mediatorHandler.SendCommand<AdminUpdateDocumentCommand>(command);
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

        [HttpPut("topic")]
        public async Task<IActionResult> PutAsync([FromBody] AdminUpdateTopicCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var validationResult = await _mediatorHandler.SendCommand<AdminUpdateTopicCommand>(command);
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

        [HttpPut("video")]
        public async Task<IActionResult> PutAsync([FromBody] AdminUpdateVideoCommand command)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var validationResult = await _mediatorHandler.SendCommand<AdminUpdateVideoCommand>(command);
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
 
