using API.Studying.Application.Commands;
using API.Studying.Application.Commands.Account;
using API.Studying.Application.Commands.Admin;
using API.Studying.Application.Commands.Document;
using API.Studying.Application.Commands.Student;
using API.Studying.Application.Commands.Topic;
using API.Studying.Application.Commands.Video;
using API.Studying.Application.DTOs;
using API.Studying.Application.Handlers;
using API.Studying.Application.Mediator;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace API.Studying.Config
{
    public static class MediatorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IRequestHandler<AccountLoginCommand, TokenDto>, AccountHandler>();
            services.AddScoped<IRequestHandler<ConfirmEmailCommand, ValidationResult>, AccountHandler>();
            services.AddScoped<IRequestHandler<ChangePasswordCommand, ValidationResult>, AccountHandler>();
            services.AddScoped<IRequestHandler<RecoverPasswordCommand, ValidationResult>, AccountHandler>();
            services.AddScoped<IRequestHandler<RecoverPasswordConfirmedCommand, ValidationResult>, AccountHandler>();
            
            services.AddScoped<IRequestHandler<CreateDocumentCommand, ValidationResult>, DocumentHandler>();
            services.AddScoped<IRequestHandler<FavoriteDocumentCommand, ValidationResult>, DocumentHandler>();
            services.AddScoped<IRequestHandler<RemoveDocumentCommand, ValidationResult>, DocumentHandler>();
            services.AddScoped<IRequestHandler<UnfavoriteDocumentCommand, ValidationResult>, DocumentHandler>();
            services.AddScoped<IRequestHandler<UpdateDocumentCommand, ValidationResult>, DocumentHandler>();
            services.AddScoped<IRequestHandler<CancelStarDocumentCommand, ValidationResult>, DocumentHandler>();
            services.AddScoped<IRequestHandler<StarDocumentCommand, ValidationResult>, DocumentHandler>();

            services.AddScoped<IRequestHandler<CreateStudentCommand, ValidationResult>, StudentHandler>();
            services.AddScoped<IRequestHandler<UpdateStudentCommand, ValidationResult>, StudentHandler>();
            services.AddScoped<IRequestHandler<UploadImageStudentCommand, ValidationResult>, StudentHandler>();

            services.AddScoped<IRequestHandler<FavoriteTopicCommand, ValidationResult>, TopicHandler>();
            services.AddScoped<IRequestHandler<UnfavoriteTopicCommand, ValidationResult>, TopicHandler>();

            services.AddScoped<IRequestHandler<CreateVideoCommand, ValidationResult>, VideoHandler>();
            services.AddScoped<IRequestHandler<FavoriteVideoCommand, ValidationResult>, VideoHandler>();
            services.AddScoped<IRequestHandler<RemoveVideoCommand, ValidationResult>, VideoHandler>();
            services.AddScoped<IRequestHandler<UnfavoriteVideoCommand, ValidationResult>, VideoHandler>();
            services.AddScoped<IRequestHandler<UpdateVideoCommand, ValidationResult>, VideoHandler>();
            services.AddScoped<IRequestHandler<CancelStarVideoCommand, ValidationResult>, VideoHandler>();
            services.AddScoped<IRequestHandler<StarVideoCommand, ValidationResult>, VideoHandler>();
            
            services.AddScoped<IRequestHandler<AdminBlockStudentCommand, ValidationResult>, AdminHandler>();
            services.AddScoped<IRequestHandler<AdminTurnStudentIntoAdminCommand, ValidationResult>, AdminHandler>();
            services.AddScoped<IRequestHandler<AdminCreateTopicCommand, ValidationResult>, AdminHandler>();
            services.AddScoped<IRequestHandler<AdminRemoveDocumentCommand, ValidationResult>, AdminHandler>();
            services.AddScoped<IRequestHandler<AdminRemoveStudentCommand, ValidationResult>, AdminHandler>();
            services.AddScoped<IRequestHandler<AdminRemoveTopicCommand, ValidationResult>, AdminHandler>();
            services.AddScoped<IRequestHandler<AdminRemoveVideoCommand, ValidationResult>, AdminHandler>();
            services.AddScoped<IRequestHandler<AdminUpdateDocumentCommand, ValidationResult>, AdminHandler>();
            services.AddScoped<IRequestHandler<AdminUpdateTopicCommand, ValidationResult>, AdminHandler>();
            services.AddScoped<IRequestHandler<AdminUpdateVideoCommand, ValidationResult>, AdminHandler>();        
        }
    }
}
