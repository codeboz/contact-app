using System;
using System.Collections.Generic;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class InfoTypeEntityTypeConfiguration:IEntityTypeConfiguration<InfoType>
    {        
        public static IEnumerable<InfoType> InfoTypeSeed =>
            new List<InfoType>
            {
                new InfoType{Id = 1, Name = "Phone"},
                new InfoType{Id = 2, Name = "Email"},
                new InfoType{Id = 3, Name = "Location"},
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
