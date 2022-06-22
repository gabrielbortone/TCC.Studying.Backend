using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminRemoveExerciseCommand : CommandBase
    {
        public Guid ExerciseId { get; set; }

        [JsonConstructor]
        public AdminRemoveExerciseCommand(Guid exerciseId)
        {
            ExerciseId = exerciseId;
            if (ExerciseId == null || ExerciseId == Guid.NewGuid())
            {
                ValidationResult.Errors.Add(new ValidationFailure("", "Resposta inválida!"));
            }
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
