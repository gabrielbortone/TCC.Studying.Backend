using API.Studying.Domain.Entities;
using API.Studying.Domain.Entities.PreferencesUser;
using API.Studying.Domain.Entities.PreferencesUser.Star;
using API.Studying.Domain.Entities.Reports;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace API.Studying.Data.DbContext
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Document> Document { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Topic> Topic { get; set; }
        public DbSet<Video> Video { get; set; }
        //Favoritos
        public DbSet<StudentVideo> StudentVideo { get; set; }
        public DbSet<StudentTopic> StudentTopic { get; set; }
        public DbSet<StudentDocument> StudentDocument { get; set; }
        //Stars
        public DbSet<StarDocument> StarDocument { get; set; }
        public DbSet<StarTopic> StarTopic { get; set; }
        public DbSet<StarVideo> StarVideo { get; set; }
        public DbSet<RecoverPassword> RecoverPassword { get; set; }

        //Reports
        public DbSet<DocumentViewReport> DocumentViewReport { get; set; }
        public DbSet<VideoViewReport> VideoViewReport { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(type => !string.IsNullOrEmpty(type.Namespace))
                                  .Where(type => type.GetInterfaces().Any(i => i.IsGenericType
                                  && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }

    }
}
