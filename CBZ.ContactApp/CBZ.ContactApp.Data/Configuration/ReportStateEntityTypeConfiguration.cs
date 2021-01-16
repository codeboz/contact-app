using System;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class ReportStateEntityTypeConfiguration:IEntityTypeConfiguration<ReportState>
    {
        public void Configure(EntityTypeBuilder<ReportState> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Inserted");
            builder.Property<DateTime>("Updated");

            //Relations
            builder.HasMany(rs => rs.ReportRequest);
            //Indexes
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}