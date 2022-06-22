using API.Studying.Application.Services;
using API.Studying.Application.Services.Interfaces;
using API.Studying.Application.Utils.EmailConfig;
using API.Studying.Application.Utils.PortugueseExtensions;
using API.Studying.Data.Repositories;
using API.Studying.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace API.Studying.Config
{
    public static class DI
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IdentityErrorDescriber, IdentityPortugueseMessages>();
            services.AddTransient<ISendEmailService, SendEmailService>();
            services.AddTransient<IRecoverPasswordRepository, RecoverPasswordRepository>();
            services.AddTransient<IDocumentViewReportRepository, DocumentViewReportRepository>();
            services.AddTransient<IVideoViewReportRepository, VideoViewReportRepository>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IFavoriteRepository,FavoriteRepository>();
        }
    }
}
