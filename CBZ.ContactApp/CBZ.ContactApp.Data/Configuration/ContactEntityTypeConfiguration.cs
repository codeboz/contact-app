using System;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class ContactEntityTypeConfiguration:IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {   //Shadow properties
            builder.Property<DateTime>("Inserted").ValueGeneratedOnAdd();
            builder.Property<DateTime>("Updated").ValueGeneratedOnUpdate();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Surname).IsRequired();
            //Relations
            builder.HasMany(c => c.Infos);
            //Indexes
            builder.HasIndex(c => new { c.Name, c.Surname });
        }
    }
}
