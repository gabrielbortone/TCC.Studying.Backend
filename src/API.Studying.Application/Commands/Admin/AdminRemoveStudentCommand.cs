using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminRemoveStudentCommand : CommandBase
    {
        public Guid StudentId { get; set; }

        [JsonConstructor]
        public AdminRemoveStudentCommand(Guid studentId)
        {
            StudentId = studentId;
            if (StudentId == null || StudentId == Guid.NewGuid())
            {
                ValidationResult.Errors.Add(new ValidationFailure("", "Estudante inválido!"));
            }
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
