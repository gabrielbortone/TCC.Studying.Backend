using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminBlockStudentCommand : CommandBase
    {
        public Guid StudentId { get; set; }


        [JsonConstructor]
        public AdminBlockStudentCommand(Guid studentId)
        {
            StudentId = studentId;
            ValidationResult = new ValidationResult();
        }

        public override bool IsValid()
        {
            if (StudentId.Equals(Guid.Empty))
            {
                ValidationResult.Errors.Add(new ValidationFailure("StudentId", "Estudante Inválido!"));
            }
            return ValidationResult.IsValid;
        }
    }
}
