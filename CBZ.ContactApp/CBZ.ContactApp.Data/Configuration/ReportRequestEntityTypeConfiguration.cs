using System;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class ReportRequestEntityTypeConfiguration:IEntityTypeConfiguration<ReportRequest>
    {
        public void Configure(EntityTypeBuilder<ReportRequest> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Requested").ValueGeneratedOnAdd();
            builder.Property<int>("ReportStateId");
            //Relations
            builder.HasOne(rr => rr.ReportState);
        }
    }
}
