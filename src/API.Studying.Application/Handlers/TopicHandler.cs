using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Topic;
using API.Studying.Domain.Entities;
using API.Studying.Domain.Interfaces.Repositories;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Studying.Application.Handlers
{
    public class TopicHandler : CommandHandler,
                                IRequestHandler<FavoriteTopicCommand, ValidationResult>,
                                IRequestHandler<UnfavoriteTopicCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TopicHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<ValidationResult> Handle(FavoriteTopicCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                var topic = _unitOfWork.TopicRepository.GetById(request.TopicId);

                if (student == null || topic == null)
                {
                    AdicionarErro("Estudante/Tópico inválido!");
                    return Task.FromResult(ValidationResult);
                }

                _unitOfWork.FavoriteRepository.FavoriteTopic(student.Id, topic.Id);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        public Task<ValidationResult> Handle(UnfavoriteTopicCommand request, CancellationToken cancellationToken)
        {
            bool IsValid = request.IsValid();
            ValidationResult = request.ValidationResult;

            if (IsValid)
            {
                var student = _unitOfWork.StudentRepository.GetById(request.StudentId);
                var topic = _unitOfWork.TopicRepository.GetById(request.TopicId);

                if (student == null || topic == null)
                {
                    AdicionarErro("Estudante/Tópico inválido!");
                    return Task.FromResult(ValidationResult);
                }

                _unitOfWork.FavoriteRepository.UnfavoriteTopic(student.Id, topic.Id);

                if (!_unitOfWork.Commit())
                    AdicionarErro("Erro ao gravar!");
            }

            return Task.FromResult(ValidationResult);
        }

        
    }
}
