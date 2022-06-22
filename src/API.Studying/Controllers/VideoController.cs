using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Video;
using API.Studying.Application.DTOs;
using API.Studying.Application.Mediator;
using API.Studying.Application.Services;
using API.Studying.Application.Utils.Jwt;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Entities.Reports;
using API.Studying.Domain.Interfaces.Repositories;
using API.Studying.Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;

namespace API.Studying.Controllers
{
    [Authorize("Bearer")]
    [Route("video")]
    public class VideoController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUnitOfWork _unitOfWork;
        public VideoController(IMediatorHandler mediatorHandler,
                               IUnitOfWork unitOfWork)
        {
            _mediatorHandler = mediatorHandler;
            _unitOfWork = unitOfWork;
        }

        private Video UpdateStats(string accessToken, Video video)
        {
            _unitOfWork.VideoRepository.UpdateViews(video);

            Guid studentId = Guid.Parse(TokenService.GetStudentId(accessToken.ToString().Replace("Bearer ", "")));
            var student = _unitOfWork.StudentRepository.GetById(studentId);

            _unitOfWork.VideoViewReportRepository.Create(new VideoViewReport(student, video, DateTime.Now));

            video.IsFavorite = _unitOfWork.FavoriteRepository.GetVideoByIds(studentId, video.Id) != null;

            return video;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var video = _unitOfWork.VideoRepository.GetById(id);
                if (video == null)
                {
                    throw new ArgumentException("Não foi possível encontrar um vídeo com esse ID!");
                }

                var videoViewModel = new VideoViewModel(video.Id, new TopicViewModel(video.Topic.Id, video.Topic.Title, video.Topic.Description),
                    video.Order, video.Title, video.UrlVideo, video.Star, video.Keys, video.Views, video.Thumbnail,
                    new StudentViewModel(video.Student.Id, video.Student.Name.FirstName, video.Student.Name.LastName, video.Student.UrlImage, video.Student.IsDeleted, video.Student.IsBlocked));

                videoViewModel.IsFavorite = _unitOfWork.FavoriteRepository.GetVideoByIds(Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""))), video.Id) != null;
                videoViewModel.IsStar = _unitOfWork.StarRepository.GetVideo(Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""))), video.Id) != null;

                return Ok(videoViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost("updateViews")]
        public IActionResult UpdateViews([FromBody] UpdateVideoViewsDto updateVideoViews)
        {
            try
            {
                var video = _unitOfWork.VideoRepository.GetById(updateVideoViews.VideoId);
                if (video == null)
                {
                    throw new ArgumentException("Não foi possível encontrar um vídeo com esse ID!");
                }

                video = UpdateStats(Request.Headers[HeaderNames.Authorization], video);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpGet("getByTopic/{topicId}")]
        public IActionResult GetByTopic(Guid topicId)
        {
            try
            {
                var videos = _unitOfWork.VideoRepository.GetById(topicId);
                return Ok(videos);
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
                var videos = _unitOfWork.VideoRepository.GetAll();
                return Ok(videos);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpGet("getBySearch")]
        public IActionResult GetBySearch(VideoSearchDto searchDto)
        {
            try
            {
                var videos = _unitOfWork.VideoRepository.GetBySearch(searchDto.Key.ToLower());
                return Ok(videos);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync ([FromBody] CreateVideoCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<CreateVideoCommand>(command);
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

        [HttpPost("Star")]
        public async Task<IActionResult> Star([FromBody] StarVideoCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<StarVideoCommand>(command);
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

        [HttpPost("CancelStar")]
        public async Task<IActionResult> CancelStar([FromBody] CancelStarVideoCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<CancelStarVideoCommand>(command);
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

        [HttpPut]
        public async Task<IActionResult> PutAsync ([FromBody] UpdateVideoCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<UpdateVideoCommand>(command);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync ([FromBody] RemoveVideoCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<RemoveVideoCommand>(command);
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