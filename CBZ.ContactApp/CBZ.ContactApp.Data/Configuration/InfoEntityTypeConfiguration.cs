using System;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class InfoEntityTypeConfiguration:IEntityTypeConfiguration<Info>
    {
        public void Configure(EntityTypeBuilder<Info> builder)
        {   
            //Shadow properties
            builder.Property<DateTime>("Inserted");
            builder.Property<DateTime>("Updated");
            builder.Property(i => i.Data).IsRequired();
            //Relations
            builder.HasOne(i => i.InfoType);
            builder.HasOne(i => i.Contact);
            //Indexes
            builder.HasKey(i => new {i.ContactId, i.InfoTypeId});
        }
    }
}