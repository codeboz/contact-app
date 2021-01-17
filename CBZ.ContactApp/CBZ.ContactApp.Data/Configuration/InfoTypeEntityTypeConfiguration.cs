using System;
using System.Collections.Generic;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class InfoTypeEntityTypeConfiguration:IEntityTypeConfiguration<InfoType>
    {        
        public static IEnumerable<object> InfoTypeSeed =>
            new List<object>
            {
                new {Id = 1, Name = "Phone", Inserted=DateTime.Now, Updated=DateTime.Now},
                new {Id = 2, Name = "Email", Inserted=DateTime.Now, Updated=DateTime.Now},
                new {Id = 3, Name = "Location", Inserted=DateTime.Now, Updated=DateTime.Now},
            };
        public void Configure(EntityTypeBuilder<InfoType> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Inserted").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
            builder.Property<DateTime>("Updated").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
            builder.Property(i => i.Name).IsRequired();
            //Relations
            builder.HasMany(it => it.Infos);
            //Indexes
            builder.HasIndex(it => it.Name).IsUnique();
            //Seed
            builder.HasData(InfoTypeSeed);

        }
    }
}
