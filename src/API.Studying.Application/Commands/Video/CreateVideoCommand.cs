using System;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace API.Studying.Application.Commands
{
    public class CreateVideoCommand : CommandBase
    {
        public Guid TopicId { get; private set; }
        public int Order { get; private set; }
        public string Title { get; private set; }
        public string UrlVideo { get; private set; }
        public string Keys { get; private set; }
        public string Thumbnail { get; private set; }
        public Guid StudentId { get; set; }

        [JsonConstructor]
        public CreateVideoCommand(Guid topicId, int order, string title, string urlVideo, string keys, string thumbnail, Guid studentId)
        {
            TopicId = topicId;
            Order = order;
            Title = title;
            UrlVideo = urlVideo;
            Keys = keys;
            Thumbnail = thumbnail;
            StudentId = studentId;
        }
        public override bool IsValid()
        {
            ValidationResult = new CreateVideoCommandValidator().Validate(this);
            var resultLinkValidation = Regex.IsMatch(UrlVideo, @"(?:.+?)?(?:\/v\/|watch\/|\?v=|\&v=|youtu\.be\/|\/v=|^youtu\.be\/|watch\%3Fv\%3D)([a-zA-Z0-9_-]{11})+");
            if (!resultLinkValidation)
            {
                ValidationResult.Errors.Add(new ValidationFailure("UrlVideo", "O pattern não é válido!"));
            }
            return ValidationResult.IsValid;
        }
    }

    public class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoCommandValidator()
        { 
            RuleFor(c => c.TopicId)
                .NotEqual(Guid.Empty)
                .WithMessage("O tópico precisa ser válido");

            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("O estudante precisa ser válido");

            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("O título precisa conter menos de 125 caracteres");
            
            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O título é obrigatório");
            
            RuleFor(c => c.UrlVideo)
                .MaximumLength(250)
                .WithMessage("A url do vídeo precisa ter menos de 250 caracteres");
            
            RuleFor(c => c.UrlVideo)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A url do vídeo é obrigatória");

            RuleFor(c => c.Keys)
                .MaximumLength(128)
                .WithMessage("As palavras-chaves devem ter menos de 128 caracteres");
        }
    }
}