using API.Studying.Domain.Entities.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Studying.Data.Mappings
{
    public class DocumentViewReportsMapping : IEntityTypeConfiguration<DocumentViewReport>
    {
        public void Configure(EntityTypeBuilder<DocumentViewReport> builder)
        {
            builder.HasOne(r => r.Student).WithMany(s => s.DocumentViewReports);
            builder.HasOne(r => r.Document).WithMany(d => d.DocumentViewReports);
        }
    }
}
