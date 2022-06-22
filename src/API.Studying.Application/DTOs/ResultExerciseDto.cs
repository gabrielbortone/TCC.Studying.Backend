using FluentValidation.Results;
using System;

namespace API.Studying.Application.DTOs
{
    public class ResultExerciseDto : ValidationResult
    {
        public Guid? ExerciseId { get; set; }
    }
}
