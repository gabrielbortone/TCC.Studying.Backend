using System;
using System.Text.Json.Serialization;

namespace API.Studying.Application.DTOs
{
    public class UpdateDocumentViewsDto
    {
        public Guid DocumentId { get; private set; }

        [JsonConstructor]
        public UpdateDocumentViewsDto(Guid documentId)
        {
            DocumentId = documentId;
        }
    }
}
