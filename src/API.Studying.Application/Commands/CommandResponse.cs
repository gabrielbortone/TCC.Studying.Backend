using FluentValidation.Results;

namespace API.Studying.Application.Commands
{
    public class CommandResponse
    {
        public ValidationResult ValidationResult { get; set; }
        public bool IsSucessfull { get; set; }
        public CommandResponse(ValidationResult validationResult, bool isSucessfull)
        {
            ValidationResult = validationResult;
            if (ValidationResult.IsValid)
                IsSucessfull = true;
            else
                IsSucessfull = false;
        }
    }
}
