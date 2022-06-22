using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Video;
using API.Studying.Application.Utils.Uploads;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Entities.Reports;
using API.Studying.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Studying.Application.Handlers
{
    public class VideoHandler : CommandHandler,
                                IRequestHandler<CreateVideoCommand, ValidationResult>,
                                IRequestHandler<FavoriteVideoCommand, ValidationResult>,
                                IRequestHandler<RemoveVideoCommand, ValidationResult>,
                                IRequestHandler<UnfavoriteVideoCommand, ValidationResult>,
                                IRequestHandler<UpdateVideoCommand, ValidationResult>,
                                IRequestHandler<StarVideoCommand, ValidationResult>,
                                IRequestHandler<CancelStarVideoCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IConfiguration _configuration;

        public VideoHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<ValidationResult> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var topic = _unitOfWork.TopicRepository.GetById(request.TopicId);
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                if (topic == null || student == null)
                {
                    AdicionarErro("Tópico/Estudante inválido!");
                    return ValidationResult;
                }

                var thumbnailUrl = string.Empty;

                try
                {
                    if (request.Thumbnail.StartsWith("https"))
                    {
                        thumbnailUrl = request.Thumbnail;
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

                var video = new Video(topic, request.Order, request.Title, request.UrlVideo, 0, request.Keys, thumbnailUrl,student, new List<VideoViewReport>());

                _unitOfWork.VideoRepository.Create(video);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return ValidationResult;
        }

        public Task<ValidationResult> Handle(FavoriteVideoCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                var video = _unitOfWork.VideoRepository.GetById(request.VideoId);

                if (student == null || video == null)
                {
                    AdicionarErro("Estudante/Vídeo inválido!");
                    return Task.FromResult(ValidationResult);
                }

                _unitOfWork.FavoriteRepository.FavoriteVideo(student.Id, video.Id);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(RemoveVideoCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                _unitOfWork.VideoRepository.Delete(request.VideoId, request.StudentId);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(UnfavoriteVideoCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                var video = _unitOfWork.VideoRepository.GetById(request.VideoId);

                if (student == null || video == null)
                {
                    AdicionarErro("Estudante/Vídeo inválido!");
                    return Task.FromResult(ValidationResult);
                }

                _unitOfWork.FavoriteRepository.UnfavoriteVideo(student.Id, video.Id);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public async Task<ValidationResult> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var topic = _unitOfWork.TopicRepository.GetById(request.TopicId);
                var videoOld = _unitOfWork.VideoRepository.GetById(request.Id);
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);

                if(videoOld == null)
                {
                    AdicionarErro("Id inválido!");
                    return ValidationResult;
                }

                if (topic == null || student == null)
                {
                    AdicionarErro("Tópico/Estudante inválido!");
                    return ValidationResult;
                }

                if (!videoOld.Student.Id.Equals(request.StudentId))
                {
                    AdicionarErro("Estudante sem essa permissão!");
                    return ValidationResult;
                }

                var thumbnailUrl = string.Empty;

                try
                {
                    if (string.IsNullOrEmpty(request.Thumbnail))
                    {
                        thumbnailUrl = videoOld.Thumbnail;
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

                videoOld.UpdateParams(topic, request.Order, request.Title, request.UrlVideo, request.Keys, thumbnailUrl);

                _unitOfWork.VideoRepository.Update(videoOld);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return ValidationResult;
        }

        public Task<ValidationResult> Handle(StarVideoCommand request, CancellationToken cancellationToken)
        {
            var video = _unitOfWork.VideoRepository.GetById(request.VideoId);
            var student = _unitOfWork.StudentRepository.GetById(request.StudentId);

            if (video == null || student == null)
            {
                AdicionarErro("Algum parâmetro inexistente!");
                return Task.FromResult(this.ValidationResult);
            }

            _unitOfWork.StarRepository.StarVideo(request.StudentId, request.VideoId);

            _unitOfWork.VideoRepository.UpdateStars(request.VideoId, 1);

            if (!_unitOfWork.Commit())
                AdicionarErro("Erro ao gravar!");

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(CancelStarVideoCommand request, CancellationToken cancellationToken)
        {
            var video = _unitOfWork.VideoRepository.GetById(request.VideoId);
            var student = _unitOfWork.StudentRepository.GetById(request.StudentId);

            if (video == null || student == null)
            {
                AdicionarErro("Algum parâmetro inexistente!");
                return Task.FromResult(this.ValidationResult);
            }

            _unitOfWork.StarRepository.CancelStarVideo(request.StudentId, request.VideoId);

            _unitOfWork.VideoRepository.UpdateStars(request.VideoId, -1);

            if (!_unitOfWork.Commit())
                AdicionarErro("Erro ao gravar!");

            return Task.FromResult(ValidationResult);
        }
    }
}
