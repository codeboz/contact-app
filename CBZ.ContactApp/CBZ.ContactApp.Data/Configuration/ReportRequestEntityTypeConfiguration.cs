using System;
using System.Collections.Generic;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class ReportRequestEntityTypeConfiguration:IEntityTypeConfiguration<ReportRequest>
    {
        public static IEnumerable<ReportRequest> ReportRequestSeed =>
            new List<ReportRequest>
            {
                new ReportRequest{Id = Guid.Parse("bf5bd4b9-ebf5-41a8-a597-69b7aeb41432"), Location = "Ankara",ReportStateId = 1, ReportId = 1},
                new ReportRequest{Id = Guid.Parse("4c802ae2-5292-4063-b5ca-e09b18871143"), Location = "Bursa",ReportStateId = 2,ReportId = 2},
            };
        public void Configure(EntityTypeBuilder<ReportRequest> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Requested").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
            builder.Property(rr => rr.Id).ValueGeneratedOnAdd();
            //Relations
            builder.HasOne(rr => rr.ReportState);
            builder.HasOne(rr => rr.Report);
            builder.HasKey(rr => rr.Id);
            //Seed
            builder.HasData(ReportRequestSeed);
        }
    }
}
