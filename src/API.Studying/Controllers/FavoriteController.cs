using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Document;
using API.Studying.Application.Commands.Topic;
using API.Studying.Application.Commands.Video;
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
    [Route("[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteController(IMediatorHandler mediatorHandler,
                                  IFavoriteRepository favoriteRepository)
        {
            _mediatorHandler = mediatorHandler;
            _favoriteRepository = favoriteRepository;
        }

        [HttpPost("document")]
        public async Task<IActionResult> PostDocumentAsync([FromBody] FavoriteDocumentCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<FavoriteDocumentCommand>(command);
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
        public async Task<IActionResult> DeleteDocumentAsync([FromBody] UnfavoriteDocumentCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<UnfavoriteDocumentCommand>(command);
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

    

        [HttpPost("topic")]
        public async Task<IActionResult> PostTopicAsync([FromBody] FavoriteTopicCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<FavoriteTopicCommand>(command);
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
        public async Task<IActionResult> DeleteTopicAsync([FromBody] UnfavoriteTopicCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<UnfavoriteTopicCommand>(command);
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

        [HttpPost("video")]
        public async Task<IActionResult> PostVideoAsync([FromBody] FavoriteVideoCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<FavoriteVideoCommand>(command);
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
        public async Task<IActionResult> DeleteVideoAsync([FromBody] UnfavoriteVideoCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<UnfavoriteVideoCommand>(command);
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
