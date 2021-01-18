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
            builder.Property<DateTime>("Inserted").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            builder.Property<DateTime>("Updated").HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();
            builder.Property(i => i.Name).IsRequired();
            builder.Property(i => i.Id).HasIdentityOptions(startValue: 100);
            //Relations
            builder.HasMany(it => it.Infos);
            //Indexes
            builder.HasIndex(it => it.Name).IsUnique();
            builder.HasKey(it => it.Id);
            //Seed
            builder.HasData(InfoTypeSeed);

        }
    }
}
