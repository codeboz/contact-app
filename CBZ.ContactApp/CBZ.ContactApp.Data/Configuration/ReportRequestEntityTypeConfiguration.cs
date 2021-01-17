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
        public static IEnumerable<object> ReportRequestSeed =>
            new List<object>
            {
                new {Id = Guid.NewGuid(), ReportStateId = 1, Location = "Ankara"},
                new {Id = Guid.NewGuid(), ReportStateId = 2, Location = "Bursa"},
            };
        public void Configure(EntityTypeBuilder<ReportRequest> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Requested").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
            builder.Property<int>("ReportStateId");
            //Relations
            builder.HasOne(rr => rr.ReportState);
            //Seed
            builder.HasData(ReportRequestSeed);
        }
    }
}
