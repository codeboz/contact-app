using System;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class InfoTypeEntityTypeConfiguration:IEntityTypeConfiguration<InfoType>
    {
        public void Configure(EntityTypeBuilder<InfoType> builder)
        {
            //Shadow properties
            builder.Property<DateTime>("Inserted").ValueGeneratedOnAdd();
            builder.Property<DateTime>("Updated").ValueGeneratedOnUpdate();
            builder.Property(i => i.Name).IsRequired();
            //Relations
            builder.HasMany(it => it.Infos);
            //Indexes
            builder.HasIndex(it => it.Name).IsUnique();

        }
    }
}
