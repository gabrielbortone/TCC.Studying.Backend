using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Document;
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
    [Authorize]
    [Route("document")]
    public class DocumentController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUnitOfWork _unitOfWork;
        public DocumentController(IMediatorHandler mediatorHandler,
                                  IUnitOfWork unitOfWork)
        {
            _mediatorHandler = mediatorHandler;
            _unitOfWork = unitOfWork;
        }

        private Document UpdateStats(string accessToken, Document document)
        {
            _unitOfWork.DocumentRepository.UpdateViews(document);
            
            Guid studentId = Guid.Parse(TokenService.GetStudentId(accessToken.ToString().Replace("Bearer ", "")));
            var student = _unitOfWork.StudentRepository.GetById(studentId);

            _unitOfWork.DocumentViewReportRepository.Create(new DocumentViewReport(student, document, DateTime.Now));

            document.IsFavorite = _unitOfWork.FavoriteRepository.GetDocumentByIds(studentId, document.Id) != null;
            
            return document;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var document = _unitOfWork.DocumentRepository.GetById(id);
                if (document == null)
                {
                    throw new ArgumentException("Não foi possível encontrar um documento com esse ID!");
                }

                var documentViewModel = new DocumentViewModel(document.Id, document.Title, document.UrlDocument, document.Stars,
                    document.Keys, document.Views, new TopicViewModel(document.Topic.Id, document.Topic.Title, document.Topic.Description),
                    new StudentViewModel(document.Student.Id, document.Student.Name.FirstName, document.Student.Name.LastName, document.Student.UrlImage, document.Student.IsDeleted, document.Student.IsBlocked));

                documentViewModel.IsFavorite = _unitOfWork.FavoriteRepository.GetDocumentByIds(Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""))), document.Id) != null;
                documentViewModel.IsStar = _unitOfWork.StarRepository.GetDocument(Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""))), document.Id) != null;

                return Ok(documentViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost("updateViews")]
        public IActionResult UpdateViews([FromBody] UpdateDocumentViewsDto updateDocument)
        {
            try
            {
                var document = _unitOfWork.DocumentRepository.GetById(updateDocument.DocumentId);
                if (document == null)
                {
                    throw new ArgumentException("Não foi possível encontrar um documento com esse ID!");
                }

                document = UpdateStats(Request.Headers[HeaderNames.Authorization], document);

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
                var documents = _unitOfWork.DocumentRepository.GetByTopic(topicId);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpGet("getBySearch")]
        public IActionResult GetBySearch(DocumentSearchDto searchDto)
        {
            try
            {
                var documents = _unitOfWork.DocumentRepository.GetBySearch(searchDto.Key.ToLower());
                return Ok(documents);
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
                var documents = _unitOfWork.DocumentRepository.GetAll();
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpGet("getByStudent/{studentId}")]
        public IActionResult GetByStudent(Guid studentId)
        {
            try
            {
                var documents = _unitOfWork.DocumentRepository.GetByStudent(studentId);
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync ([FromBody] CreateDocumentCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<CreateDocumentCommand>(command);
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
        public async Task<IActionResult> Star([FromBody] StarDocumentCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<StarDocumentCommand>(command);
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
        public async Task<IActionResult> CancelStar([FromBody] CancelStarDocumentCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<CancelStarDocumentCommand>(command);
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
        public async Task<IActionResult> PutAsync ([FromBody] UpdateDocumentCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<UpdateDocumentCommand>(command);
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
        public async Task<IActionResult> DeleteAsync ([FromBody]RemoveDocumentCommand command)
        {
            try
            {
                var studentId = Guid.Parse(TokenService.GetStudentId(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "")));
                command.StudentId = studentId;
                var validationResult = await _mediatorHandler.SendCommand<RemoveDocumentCommand>(command);
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