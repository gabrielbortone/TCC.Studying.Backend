using API.Studying.Application.Services;
using API.Studying.Application.Utils.Jwt;
using API.Studying.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;

namespace API.Studying.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("topicByStudent")]
        public IActionResult GetTopics()
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                var topics = _unitOfWork.TopicRepository.GetByStudentId(studentId);
                return Ok(topics);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }
    }
}
