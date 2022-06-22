using System;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace API.Studying.Application.Commands
{
    public class UpdateVideoCommand : CommandBase
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string UrlVideo { get; set; }
        public string Keys { get; set; }
        public string Thumbnail { get; set; }
        public Guid StudentId { get; set; }

        [JsonConstructor]
        public UpdateVideoCommand(Guid id, Guid topicId, Guid studentId,int order, string title, string urlVideo, string keys, string thumbnail)
        {
            Id = id;
            TopicId = topicId;
            StudentId = studentId;
            Order = order;
            Title = title;
            UrlVideo = urlVideo;
            Keys = keys;
            Thumbnail = thumbnail;
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateVideoCommandValidator().Validate(this);
            var resultLinkValidation = Regex.IsMatch(UrlVideo, @"(?:.+?)?(?:\/v\/|watch\/|\?v=|\&v=|youtu\.be\/|\/v=|^youtu\.be\/|watch\%3Fv\%3D)([a-zA-Z0-9_-]{11})+");
            if (!resultLinkValidation)
            {
                ValidationResult.Errors.Add(new ValidationFailure("UrlVideo", "O pattern não é válido!"));
            }
            return ValidationResult.IsValid;
        }
    }

    public class UpdateVideoCommandValidator : AbstractValidator<UpdateVideoCommand>
    {
        public UpdateVideoCommandValidator()
        { 
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O id precisa ser válido");

            RuleFor(c => c.TopicId)
                .NotEqual(Guid.Empty)
                .WithMessage("O tópico precisa ser válido");

            RuleFor(c => c.StudentId)
                .NotEqual(Guid.Empty)
                .WithMessage("O estudante precisa ser válida");

            RuleFor(c => c.Title)
                .MaximumLength(125)
                .WithMessage("O título deve ter menos de 125 caracteres");
            
            RuleFor(c => c.Title)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("O título é obrigatório");
            
            RuleFor(c => c.UrlVideo)
                .MaximumLength(250)
                .WithMessage("A url do vídeo precisa ser menor que 250 caracteres");
            
            RuleFor(c => c.UrlVideo)
                .NotEqual(string.Empty).NotNull()
                .WithMessage("A url do vídeo é necessária");

            RuleFor(c => c.Keys)
                .MaximumLength(128)
                .WithMessage("As palavras-chaves devem ter menos de 128 caracteres");
        }
    }
}