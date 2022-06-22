using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Document;
using API.Studying.Application.Utils.Uploads;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Entities.Reports;
using API.Studying.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Studying.Application.Handlers
{
    public class DocumentHandler : CommandHandler,
                                    IRequestHandler<CreateDocumentCommand, ValidationResult>,
                                    IRequestHandler<FavoriteDocumentCommand, ValidationResult>,
                                    IRequestHandler<RemoveDocumentCommand, ValidationResult>,
                                    IRequestHandler<UnfavoriteDocumentCommand, ValidationResult>,
                                    IRequestHandler<UpdateDocumentCommand, ValidationResult>,
                                    IRequestHandler<StarDocumentCommand, ValidationResult>,
                                    IRequestHandler<CancelStarDocumentCommand, ValidationResult>

    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public DocumentHandler(IUnitOfWork unitOfWork,
                                IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        private string GetUrlString(ValidationResult validationResult, string documentBase64)
        {
            try
            {
                return UploadDocumentService.UploadBase64Document(documentBase64, _configuration["AzureBlob:Connection"], _configuration["AzureBlob:DocumentContainer"]);
            }
            catch
            {
                validationResult.Errors.Add(new ValidationFailure("DocumentBase64", "Erro no upload do arquivo! Verifique tudo e tente novamente!"));
                return "";
            }
        }

        private string GetUrlStringUpdate(string oldUrl, string documentBase64)
        {
            try
            {
                return UploadDocumentService.UploadBase64Document(documentBase64, _configuration["AzureBlob:Connection"], _configuration["AzureBlob:DocumentContainer"]);
            }
            catch
            {
                return oldUrl;
            }
        }

        public Task<ValidationResult> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;


            if (IsValid)
            {
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                var topic = _unitOfWork.TopicRepository.GetById(request.TopicId);
                
                if (student == null || topic == null)
                {
                    AdicionarErro("Tópico/Estudante inválido!");
                    return Task.FromResult(ValidationResult);
                }

                var documentUrl = GetUrlString(ValidationResult, request.DocumentBase64);

                if (string.IsNullOrEmpty(documentUrl))
                    return Task.FromResult(ValidationResult);

                var document = new Document(topic, student, request.Title, documentUrl, 0, request.Keys, new List<DocumentViewReport>());
                

                _unitOfWork.DocumentRepository.Create(document);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(FavoriteDocumentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                var document = _unitOfWork.DocumentRepository.GetById(request.DocumentId);

                if (student == null || document == null)
                {
                    AdicionarErro("Estudante/Documento inválido!");
                }

                _unitOfWork.FavoriteRepository.FavoriteDocument(student.Id, document.Id);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(RemoveDocumentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                _unitOfWork.DocumentRepository.Delete(request.DocumentId, request.StudentId);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(UnfavoriteDocumentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                var document = _unitOfWork.DocumentRepository.GetById(request.DocumentId);

                if (student == null || document == null)
                {
                    AdicionarErro("Estudante/Documento inválido!");
                    return Task.FromResult(ValidationResult);
                }

                _unitOfWork.FavoriteRepository.UnfavoriteDocument(student.Id, document.Id);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var topic = _unitOfWork.TopicRepository.GetById(request.TopicId);
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                var oldDocument = _unitOfWork.DocumentRepository.GetById(request.Id);

                if (oldDocument == null)
                {
                    AdicionarErro("Id do documento inválido!");
                    return Task.FromResult(ValidationResult);
                }

                if (topic == null || student == null)
                {
                    AdicionarErro("Estudante/Tópico inválido!");
                    return Task.FromResult(ValidationResult);
                }

                if (!oldDocument.Student.Id.Equals(student.Id))
                {
                    AdicionarErro("Estudante sem essa permissão!");
                    return Task.FromResult(ValidationResult);
                }

                var documentUrl = oldDocument.UrlDocument;

                if (!string.IsNullOrEmpty(request.UrlDocument))
                {
                    documentUrl = GetUrlStringUpdate(oldDocument.UrlDocument, request.UrlDocument);
                }
                

                oldDocument.UpdateParams(topic, request.Title, documentUrl, request.Keys);
                
                _unitOfWork.DocumentRepository.Update(oldDocument);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(StarDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _unitOfWork.DocumentRepository.GetById(request.DocumentId);
            var student = _unitOfWork.StudentRepository.GetById(request.StudentId);

            if (document == null || student == null)
            {
                AdicionarErro("Algum parâmetro inexistente!");
                return Task.FromResult(this.ValidationResult);
            }
                
            _unitOfWork.StarRepository.StarDocument(request.StudentId, request.DocumentId);

            _unitOfWork.DocumentRepository.UpdateStars(request.DocumentId, 1);

            if (!_unitOfWork.Commit())
                AdicionarErro("Erro ao gravar!");

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(CancelStarDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _unitOfWork.DocumentRepository.GetById(request.DocumentId);
            var student = _unitOfWork.StudentRepository.GetById(request.StudentId);

            if (document == null || student == null)
            {
                AdicionarErro("Algum parâmetro inexistente!");
                return Task.FromResult(this.ValidationResult);
            }

            _unitOfWork.StarRepository.CancelStarDocument(request.StudentId, request.DocumentId);
            
            _unitOfWork.DocumentRepository.UpdateStars(request.DocumentId, -1);

            if (!_unitOfWork.Commit())
                AdicionarErro("Erro ao gravar!");

            return Task.FromResult(ValidationResult);
        }
    }
}
