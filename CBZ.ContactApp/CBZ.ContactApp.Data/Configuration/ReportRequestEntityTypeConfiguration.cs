using System;
using System.Collections;
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
                new ReportRequest{Id = Guid.NewGuid(), Location = "Ankara",ReportStateId = 1},
                new ReportRequest{Id = Guid.NewGuid(), Location = "Bursa",ReportStateId = 2},
            };
        public void Configure(EntityTypeBuilder<ReportRequest> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Requested").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
            //Relations
            builder.HasOne(rr => rr.ReportState);
            //Seed
            builder.HasData(ReportRequestSeed);
        }
    }
}
