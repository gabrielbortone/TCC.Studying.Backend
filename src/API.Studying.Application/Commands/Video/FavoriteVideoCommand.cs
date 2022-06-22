using System;
using FluentValidation;
using Newtonsoft.Json;

namespace API.Studying.Application.Commands
{
    public class FavoriteVideoCommand : CommandBase
    {
        public Guid StudentId { get; set; }
        public Guid VideoId {get; private set;}

        [JsonConstructor]
        public FavoriteVideoCommand(Guid studentId, Guid videoId)
        {
            StudentId = studentId;
            VideoId = videoId;
        }
        public override bool IsValid()
        {
            ValidationResult = new FavoriteVideoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

      public class FavoriteVideoCommandValidator : AbstractValidator<FavoriteVideoCommand>
    {
        public FavoriteVideoCommandValidator()
        {
            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("O estudante precisa ser válido");
                
            RuleFor(c => c.VideoId)
                .NotEqual(Guid.Empty)
                .WithMessage("O vídeo precisa ser válido");
        }
    }
}