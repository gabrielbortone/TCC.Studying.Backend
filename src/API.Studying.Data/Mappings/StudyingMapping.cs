using API.Studying.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Studying.Data.Mappings
{
    public class StudyingMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {

            builder.OwnsOne(s => s.Name, studentName =>
            {
                studentName.Property(n => n.FirstName).HasMaxLength(30);
                studentName.Property(n => n.LastName).HasMaxLength(30);
            });

            builder.OwnsOne(s => s.CPF, studentCpf =>
            {
                studentCpf.Property(c => c.Number).HasMaxLength(11);
            });

            builder.Property(s => s.UrlImage).HasMaxLength(300);
            builder.Property(s => s.Scholarity).HasMaxLength(45);
            builder.Property(s => s.Institution).HasMaxLength(50);

            builder.HasOne(s => s.IdentityUser).WithOne(u => u.Student).IsRequired(true);

            builder.HasMany(s => s.Documents).WithOne(d => d.Student);
            builder.HasMany(s => s.Videos).WithOne(v => v.Student);
        }
    }
}
