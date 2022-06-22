using API.Studying.Application.DTOs;
using API.Studying.Application.Mediator;
using API.Studying.Application.Services;
using API.Studying.Application.Utils.Jwt;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;

namespace API.Studying.Controllers
{
    [Authorize("Bearer")]
    [Route("topic")]
    public class TopicController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUnitOfWork _unitOfWork;
        public TopicController(IMediatorHandler mediatorHandler,
                               IUnitOfWork unitOfWork)
        {
            _mediatorHandler = mediatorHandler;
            _unitOfWork = unitOfWork;
        }

        private Topic UpdateStats(string accessToken, Topic topic)
        {
            Guid studentId = Guid.Parse(TokenService.GetStudentId(accessToken.ToString().Replace("Bearer ", "")));

            topic.IsFavorite = _unitOfWork.FavoriteRepository.GetTopicByIds(studentId, topic.Id) != null;
            return topic;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var topic = _unitOfWork.TopicRepository.GetById(id);
                if(topic == null)
                {
                    throw new ArgumentException("Não existe tópico com esse id");
                }

                topic = UpdateStats(Request.Headers[HeaderNames.Authorization], topic);

                return Ok(topic);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var topics = _unitOfWork.TopicRepository.GetAll();

                return Ok(topics);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }
        
        [HttpGet("getBySearch")]
        public IActionResult GetBySearch(TopicSearchDto searchDto)
        {
            try
            {
                var topics = _unitOfWork.TopicRepository.GetBySearch(searchDto.Key.ToLower());
                return Ok(topics);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }
    }
}