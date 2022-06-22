using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Video
{
    public class UnfavoriteVideoCommand : CommandBase
    {
        public Guid StudentId { get; set; }
        public Guid VideoId { get; set; }
        [JsonConstructor]
        public UnfavoriteVideoCommand(Guid studentId, Guid videoId)
        {
            StudentId = studentId;
            VideoId = videoId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UnfavoriteVideoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UnfavoriteVideoCommandValidator : AbstractValidator<UnfavoriteVideoCommand>
    {
        public UnfavoriteVideoCommandValidator()
        {
            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("Estudante inválido");

            RuleFor(c => c.VideoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Vídeo inválido");
        }
    }
}
