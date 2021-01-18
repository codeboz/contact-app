using System;
using System.Collections.Generic;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
    {
        public static IEnumerable<Contact> ContactSeed =>
            new List<Contact>
            {
                new Contact {Company = "A", Id = Guid.Parse("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), Name = "A", Surname = "Aa"},
                new Contact {Company = "B", Id = Guid.Parse("3182b605-a8cb-4eb4-81fb-81129159f095"), Name = "B", Surname = "Bb"},
                new Contact {Company = "C", Id = Guid.Parse("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), Name = "C", Surname = "Cc"},
                new Contact {Company = "D", Id = Guid.Parse("342ea3e1-6e66-4162-a7c0-390c2a5aa263"), Name = "D", Surname = "Dd"},
                new Contact {Company = "E", Id = Guid.Parse("19c45392-88ba-4f96-92d4-c72024d48f81"), Name = "E", Surname = "Ee"},
            };
    
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            //Shadow properties
            builder.Property(contact => contact.Id).ValueGeneratedOnAdd();
            builder.Property<DateTime>("Inserted").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
            builder.Property<DateTime>("Updated").HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Surname).IsRequired();
            //Relations
            builder.HasMany(c => c.Infos);
            //Indexes
            builder.HasIndex(c => new { c.Name, c.Surname });
            builder.HasKey(c => c.Id);
            //InitialData
            builder.HasData(ContactSeed);
        }
    }
}
