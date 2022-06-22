using API.Studying.Application.Commands.Admin;
using API.Studying.Application.Utils.Uploads;
using API.Studying.Domain.Entities;
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
    public class AdminHandler : CommandHandler,
                                IRequestHandler<AdminBlockStudentCommand, ValidationResult>,
                                IRequestHandler<AdminTurnStudentIntoAdminCommand, ValidationResult>,
                                IRequestHandler<AdminCreateTopicCommand, ValidationResult>,
                                IRequestHandler<AdminRemoveDocumentCommand, ValidationResult>,
                                IRequestHandler<AdminRemoveStudentCommand, ValidationResult>,
                                IRequestHandler<AdminRemoveTopicCommand, ValidationResult>,
                                IRequestHandler<AdminRemoveVideoCommand, ValidationResult>,
                                IRequestHandler<AdminUpdateDocumentCommand, ValidationResult>,
                                IRequestHandler<AdminUpdateTopicCommand, ValidationResult>,
                                IRequestHandler<AdminUpdateVideoCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IConfiguration _configuration;

        public AdminHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public Task<ValidationResult> Handle(AdminBlockStudentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                _unitOfWork.StudentRepository.BlockStudent(request.StudentId);
            }
            else
            {
                return Task.FromResult(request.ValidationResult);
            }

            if (!_unitOfWork.Commit())
                AdicionarErro("Erro ao gravar!");

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(AdminCreateTopicCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {

                Topic topicFather = null;
                if(request.FatherTopicId != null)
                {
                    topicFather = _unitOfWork.TopicRepository.GetById((Guid)request.FatherTopicId);
                }

                var topic = new Topic(request.Title, request.Description, request.FatherTopicId,
                    topicFather,  new List<Document>(), new List<Video>());
                _unitOfWork.TopicRepository.Create(topic);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(AdminRemoveDocumentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                _unitOfWork.DocumentRepository.DeleteDocument(request.DocumentId);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(AdminRemoveStudentCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                _unitOfWork.StudentRepository.DeleteStudent(request.StudentId);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(AdminRemoveTopicCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                _unitOfWork.TopicRepository.Delete(request.TopicId, Guid.NewGuid());

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(AdminRemoveVideoCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                _unitOfWork.VideoRepository.DeleteVideo(request.VideoId);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
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
        public Task<ValidationResult> Handle(AdminUpdateDocumentCommand request, CancellationToken cancellationToken)
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

        public Task<ValidationResult> Handle(AdminUpdateTopicCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                Topic topicFather = null;

                if (request.FatherTopicId != null)
                {
                    topicFather = _unitOfWork.TopicRepository.GetById((Guid)request.FatherTopicId);
                }

                var topic = _unitOfWork.TopicRepository.GetById(request.Id);
                if(topic == null)
                {
                    AdicionarErro("Tópico inválido!");
                    return Task.FromResult(ValidationResult);
                }

                topic.UpdateParams(topicFather?.Id, topicFather, request.Title, request.Description);

                _unitOfWork.TopicRepository.Update(topic);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public async Task<ValidationResult> Handle(AdminUpdateVideoCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var topic = _unitOfWork.TopicRepository.GetById(request.TopicId);
                var video = _unitOfWork.VideoRepository.GetById(request.Id);

                if (video == null)
                {
                    AdicionarErro("Id inválido!");
                    return ValidationResult;
                }

                if (topic == null)
                {
                    AdicionarErro("Tópico inválido!");
                    return ValidationResult;
                }

                var thumbnailUrl = string.Empty;

                try
                {
                    if (string.IsNullOrEmpty(request.Thumbnail))
                    {
                        thumbnailUrl = video.Thumbnail;
                    }
                    else
                    {
                        thumbnailUrl = await UploadImageService.UploadBase64ImageAsync(request.Thumbnail, _configuration["AzureBlob:Connection"], _configuration["AzureBlob:ThumbnailContainer"]);
                    }
                }
                catch
                {
                    thumbnailUrl = string.Empty;
                }

                video.UpdateParams(topic, request.Order, request.Title, request.UrlVideo, request.Keys, thumbnailUrl);

                _unitOfWork.VideoRepository.Update(video);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(AdminTurnStudentIntoAdminCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                if(student == null)
                {
                    AdicionarErro("Estudante Inválido");
                    return ValidationResult;
                }

                if (student.IsAdmin)
                {
                    AdicionarErro("Estudante já é administrador");
                    return ValidationResult;
                }

                student.TurnStudentIntoAdmin();

                _unitOfWork.StudentRepository.Update(student);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return ValidationResult;
        }
    }
}
