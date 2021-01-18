using System;
using System.Collections.Generic;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class ReportStateEntityTypeConfiguration:IEntityTypeConfiguration<ReportState>
    {
        public static IEnumerable<ReportState> ReportStateSeed =>
            new List<ReportState>
            {
                new ReportState{Id = 1, Name = "Preparing"},
                new ReportState{Id = 2, Name = "Ready"}
            };
        public void Configure(EntityTypeBuilder<ReportState> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Inserted").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            builder.Property<DateTime>("Updated").HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();
            builder.Property(rs => rs.Name).IsRequired();
            builder.Property(rs => rs.Id).HasIdentityOptions(startValue: 100);
            //Relations
            builder.HasMany(rs => rs.ReportRequest);
            //Indexes
            builder.HasKey(rs => rs.Id);
            builder.HasIndex(rs => rs.Name).IsUnique();
            //Seed
            builder.HasData(ReportStateSeed);
        }
    }
}
