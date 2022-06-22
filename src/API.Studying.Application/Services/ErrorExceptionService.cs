using FluentValidation.Results;
using Serilog;
using System;

namespace API.Studying.Application.Services
{
    public static class ErrorExceptionService
    {
        public static ValidationResult GetValidationError(Exception ex)
        {
            var validationResultError = new ValidationResult();
            validationResultError.Errors.Add(new ValidationFailure(string.Empty, ex.Message));
            Log.Error(ex.Message);
            return validationResultError;
        }
    }
}
