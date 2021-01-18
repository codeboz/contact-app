using System;
using System.Collections.Generic;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class ReportEntityTypeConfiguration:IEntityTypeConfiguration<Report>
    {
        public static IEnumerable<Report> ReportSeed =>
            new List<Report>
            {
                new Report{Id = 1,Location = "Ankara",ContactCount = 3,PhoneNumberCount = 3},
                new Report{Id = 2,Location = "Bursa",ContactCount = 2,PhoneNumberCount = 2},
            };
        
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Inserted").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            builder.Property<DateTime>("Updated").HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();
            builder.Property(r => r.Location).IsRequired();
            builder.Property(r => r.Id).HasIdentityOptions(startValue: 100);
            //Indexes
            builder.HasKey(r => r.Id);
            //Seed
            builder.HasData(ReportSeed);
        }
    }
}
