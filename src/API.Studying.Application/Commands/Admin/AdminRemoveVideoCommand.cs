using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Studying.Application.Commands.Admin
{
    public class AdminRemoveVideoCommand : CommandBase
    {
        public Guid VideoId { get; set; }

        [JsonConstructor]
        public AdminRemoveVideoCommand(Guid videoId)
        {
            VideoId = videoId;
            this.ValidationResult = new ValidationResult();
            if (VideoId == Guid.NewGuid())
            {
                ValidationResult.Errors.Add(new ValidationFailure("", "Vídeo inválido!"));
            }
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
