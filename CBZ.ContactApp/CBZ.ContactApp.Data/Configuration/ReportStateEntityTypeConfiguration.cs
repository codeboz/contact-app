using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class ReportStateEntityTypeConfiguration:IEntityTypeConfiguration<ReportState>
    {
        public static IEnumerable<object> ReportStateSeed =>
            new List<object>
            {
                new {Id = 1, Name = "Preparing", Inserted=DateTime.Now, Updated=DateTime.Now},
                new {Id = 2, Name = "Ready", Inserted=DateTime.Now, Updated=DateTime.Now}
            };
        public void Configure(EntityTypeBuilder<ReportState> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Inserted").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
            builder.Property<DateTime>("Updated").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
            builder.Property(rs => rs.Name).IsRequired();
            //Relations
            builder.HasMany(rs => rs.ReportRequest);
            //Indexes
            builder.HasIndex(rs => rs.Name).IsUnique();
            //Seed
            builder.HasData(ReportStateSeed);
        }
    }
}
