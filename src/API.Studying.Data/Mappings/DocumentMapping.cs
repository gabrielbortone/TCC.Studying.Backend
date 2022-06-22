using API.Studying.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Studying.Data.Mappings
{
    public class DocumentMapping : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasOne(d => d.Student).WithMany(s => s.Documents);
            builder.HasOne(d => d.Topic).WithMany(t => t.Documents);
            builder.Property(d => d.Title).HasMaxLength(125);
            builder.Property(d => d.UrlDocument).HasMaxLength(400);
            builder.Property(d => d.Keys).HasMaxLength(128);
        }
    }
}
