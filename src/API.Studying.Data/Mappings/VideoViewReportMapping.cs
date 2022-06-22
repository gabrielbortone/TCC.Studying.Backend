using API.Studying.Domain.Entities.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Studying.Data.Mappings
{
    public class VideoViewReportMapping : IEntityTypeConfiguration<VideoViewReport>
    {
        public void Configure(EntityTypeBuilder<VideoViewReport> builder)
        {
            builder.HasOne(r=> r.Student).WithMany(s=> s.VideoViewReports);
            builder.HasOne(r=> r.Video).WithMany(v=> v.VideoViewReports);
        }
    }
}
