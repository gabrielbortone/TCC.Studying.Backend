using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Student;
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
    [Route("student")]
    public class StudentController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUnitOfWork _unitOfWork;
        public StudentController(IMediatorHandler mediatorHandler,
                                 IUnitOfWork unitOfWork)
        {
            _mediatorHandler = mediatorHandler;
            _unitOfWork = unitOfWork;
        }

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var student = _unitOfWork.StudentRepository.GetByIdAllInfo(id);
                if(student == null)
                {
                    throw new ArgumentException("Não existe estudante com esse id!");
                }

                student.AllFavorites = _unitOfWork.FavoriteRepository.GetAllFavoritesByStudentViewModel(id);
               
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [Authorize("Bearer")]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            try
            {
                var students = _unitOfWork.StudentRepository.GetAll();
                if (students == null)
                {
                    throw new ArgumentException("Não existe estudante!");
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [Authorize("Bearer")]
        [HttpGet("getAllExceptAdmins")]
        public IActionResult GetAllExceptAdmins()
        {
            try
            {
                var students = _unitOfWork.StudentRepository.GetAllExceptAdmins();
                if (students == null)
                {
                    throw new ArgumentException("Não existe estudante!");
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [Authorize("Bearer")]
        [HttpGet("getBySearch/{key}")]
        public IActionResult GetBySeacrch(string key)
        {
            try
            {
                var students = _unitOfWork.StudentRepository.GetBySearch(key);
                if (students == null)
                {
                    throw new ArgumentException("Não existe estudante!");
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync ([FromBody] CreateStudentCommand command)
        {
            try
            {
                var validationResult = await _mediatorHandler.SendCommand<CreateStudentCommand>(command);
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

        [Authorize("Bearer")]
        [HttpPut("upload-image")]
        public async Task<IActionResult> UpdateImageAsync([FromBody] UploadImageStudentCommand command)
        {
            try
            {
                command.StudentId = GetStudentId();

                var validationResult = await _mediatorHandler.SendCommand<UploadImageStudentCommand>(command);
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

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<IActionResult> PutAsync ([FromBody] UpdateStudentCommand command)
        {
            try
            {
                var validationResult = await _mediatorHandler.SendCommand<UpdateStudentCommand>(command);
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

        private Guid GetStudentId()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new Exception("Estudante inválido");
            }

            return Guid.Parse(TokenService.GetStudentId(accessToken.ToString().Replace("Bearer ", "")));
        }
    }
}