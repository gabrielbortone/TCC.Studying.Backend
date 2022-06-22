using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.Commands.Video
{
    public class RemoveVideoCommand : CommandBase
    {
        public Guid VideoId { get; set; }
        public Guid StudentId { get; set; }

        public RemoveVideoCommand(){}

        [JsonConstructor]
        public RemoveVideoCommand(Guid videoId, Guid studentId)
        {
            VideoId = videoId;
            StudentId = studentId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveVideoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoveVideoCommandValidator : AbstractValidator<RemoveVideoCommand>
    {
        public RemoveVideoCommandValidator()
        {
            RuleFor(c => c.VideoId)
                .NotEqual(Guid.Empty)
                .WithMessage("O vídeo precisa ser válido");

            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("O estudante precisa ser válido");
        }
    }
}
