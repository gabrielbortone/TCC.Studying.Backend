using API.Studying.Application.DTOs;
using API.Studying.Application.Mediator;
using API.Studying.Application.Services;
using API.Studying.Application.Services.Interfaces;
using API.Studying.Application.Utils.Jwt;
using API.Studying.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;

namespace API.Studying.Controllers
{
    [Authorize("Bearer")]
    [Route("reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IStudentRepository _studentRepository;
        private readonly IReportService _reportService;

        public ReportsController(IMediatorHandler mediatorHandler,
                               IStudentRepository studentRepository,
                               IReportService reportService)
        {
            _mediatorHandler = mediatorHandler;
            _studentRepository = studentRepository;
            _reportService = reportService;
        }
        private bool IsAdminCheck()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(accessToken))
            {
                return false;
            }

            Guid studentId = Guid.Parse(TokenService.GetStudentId(accessToken.ToString().Replace("Bearer ", "")));
            var student = _studentRepository.GetById(studentId);
            if (student == null)
            {
                return false;
            }
            else
            {
                return student.IsAdmin;
            }
        }

        [HttpPost("getRankingofStudents")]
        public IActionResult GetRakingofStudents([FromBody]StudentRankingDto studentRankingDto)
        {
            try
            {
                var result = _reportService.GetRakingofStudents(studentRankingDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost("getDocumentViewReport")]
        public IActionResult GetDocumentViewReport([FromBody]ReportViewDto reportViewDto)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var result = _reportService.GetDocumentViewReport(reportViewDto);
                if (result == null)
                {
                    var validationResultError = new ValidationResult();
                    validationResultError.Errors.Add(new ValidationFailure(string.Empty, "Nada a ser exibido!"));
                    return BadRequest(validationResultError);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpPost("getVideoViewReport")]
        public IActionResult GetVideoViewReport([FromBody]ReportViewDto reportViewDto)
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var result = _reportService.GetVideoViewReport(reportViewDto);
                if (result == null)
                {
                    var validationResultError = new ValidationResult();
                    validationResultError.Errors.Add(new ValidationFailure(string.Empty, "Nada a ser exibido!"));
                    return BadRequest(validationResultError);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpGet("getDocumentStarsReport")]
        public IActionResult GetDocumentStarsReport()
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var result = _reportService.GetDocumentStarsCountReport();
                if (result == null || result.Count == 0)
                {
                    var validationResultError = new ValidationResult();
                    validationResultError.Errors.Add(new ValidationFailure(string.Empty, "Nada a ser exibido!"));
                    return BadRequest(validationResultError);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpGet("getVideoStarsReport")]
        public IActionResult GetVideoStarsReport()
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var result = _reportService.GetVideoStarsCountReport();
                if (result == null || result.Count == 0)
                {
                    var validationResultError = new ValidationResult();
                    validationResultError.Errors.Add(new ValidationFailure(string.Empty, "Nada a ser exibido!"));
                    return BadRequest(validationResultError);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpGet("getMostFavoriteDocumentsReport")]
        public IActionResult GetMostFavoriteDocumentsReport()
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var result = _reportService.GetMostFavoriteDocumentsReport();
                if (result == null || result.Count == 0)
                {
                    var validationResultError = new ValidationResult();
                    validationResultError.Errors.Add(new ValidationFailure(string.Empty, "Nada a ser exibido!"));
                    return BadRequest(validationResultError);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

        [HttpGet("getMostFavoriteVideosReport")]
        public IActionResult GetMostFavoriteVideosReport()
        {
            try
            {
                if (!IsAdminCheck())
                {
                    return Unauthorized();
                }

                var result = _reportService.GetMostFavoriteVideosReport();
                if (result == null || result.Count == 0)
                {
                    var validationResultError = new ValidationResult();
                    validationResultError.Errors.Add(new ValidationFailure(string.Empty, "Nada a ser exibido!"));
                    return BadRequest(validationResultError);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorExceptionService.GetValidationError(ex));
            }
        }

    }
}
