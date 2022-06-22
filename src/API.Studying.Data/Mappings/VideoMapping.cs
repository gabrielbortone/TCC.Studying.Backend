using API.Studying.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Studying.Data.Mappings
{
    public class VideoMapping : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasOne(v => v.Student).WithMany(s => s.Videos);
            builder.HasOne(v => v.Topic).WithMany(t=> t.Videos);
            builder.Property(v => v.Keys).HasMaxLength(128);
            builder.Property(v => v.Thumbnail).HasMaxLength(256);
            builder.Property(v => v.Title).HasMaxLength(128);
            builder.Property(v => v.UrlVideo).HasMaxLength(256);
        }
    }
}
