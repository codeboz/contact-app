using System;
using System.Collections.Generic;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CBZ.ContactApp.Data.Configuration
{
    public class InfoEntityTypeConfiguration:IEntityTypeConfiguration<Info>
    {
        //new InfoType{Id = 1, Name = "Phone"},
        //new InfoType{Id = 2, Name = "Email"},
        //new InfoType{Id = 3, Name = "Location"},
        public  static IEnumerable<object> InfoSeed =>
            new List<object>
            {
                //new Contact{Company = "A",Id = Guid.Parse("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"),Name = "A",Surname = "Aa"},
                new {ContactId = Guid.Parse("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), InfoTypeId = 1, Data = "11111111",  Inserted=DateTime.Now, Updated=DateTime.Now},
                new {ContactId = Guid.Parse("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), InfoTypeId = 2, Data = "a@a.com",   Inserted=DateTime.Now, Updated=DateTime.Now},
                new {ContactId = Guid.Parse("3999bbc4-751e-4fe5-9e72-4c6e88f6cff4"), InfoTypeId = 3, Data = "Ankara",    Inserted=DateTime.Now, Updated=DateTime.Now},
                                                                                                     
                //new Contact{Company = "B",Id = Guid.Parse("3182b605-a8cb-4eb4-81fb-81129159f095"),Name = "B",Surname = "Bb"},
                new {ContactId = Guid.Parse("3182b605-a8cb-4eb4-81fb-81129159f095"), InfoTypeId = 1, Data = "222222222", Inserted=DateTime.Now, Updated=DateTime.Now},
                new {ContactId = Guid.Parse("3182b605-a8cb-4eb4-81fb-81129159f095"), InfoTypeId = 2, Data = "b@b.com",   Inserted=DateTime.Now, Updated=DateTime.Now},
                new {ContactId = Guid.Parse("3182b605-a8cb-4eb4-81fb-81129159f095"), InfoTypeId = 3, Data = "Ankara",    Inserted=DateTime.Now, Updated=DateTime.Now},
                                                                                                     
                //new Contact{Company = "C",Id = Guid.Parse("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"),Name = "C",Surname = "Cc"},
                new {ContactId = Guid.Parse("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), InfoTypeId = 1, Data = "333333333", Inserted=DateTime.Now, Updated=DateTime.Now},
                new {ContactId = Guid.Parse("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), InfoTypeId = 2, Data = "c@c.com",   Inserted=DateTime.Now, Updated=DateTime.Now},
                new {ContactId = Guid.Parse("b75cdf77-243b-4eb8-ab08-c44dcddb70ef"), InfoTypeId = 3, Data = "Ankara",    Inserted=DateTime.Now, Updated=DateTime.Now},
                
                //new Contact{Company = "D",Id = Guid.Parse("342ea3e1-6e66-4162-a7c0-390c2a5aa263"),Name = "D",Surname = "Dd"},
                new Info{ContactId = Guid.Parse("342ea3e1-6e66-4162-a7c0-390c2a5aa263"),InfoTypeId = 1,Data = "4444444444"},
                new Info{ContactId = Guid.Parse("342ea3e1-6e66-4162-a7c0-390c2a5aa263"),InfoTypeId = 2,Data = "d@d.com"},
                new Info{ContactId = Guid.Parse("342ea3e1-6e66-4162-a7c0-390c2a5aa263"),InfoTypeId = 3,Data = "Bursa"},
                
                //new Contact{Company = "E",Id = Guid.Parse("19c45392-88ba-4f96-92d4-c72024d48f81"),Name = "E",Surname = "Ee"},
                new Info{ContactId = Guid.Parse("19c45392-88ba-4f96-92d4-c72024d48f81"),InfoTypeId = 1,Data = "555555555"},
                new Info{ContactId = Guid.Parse("19c45392-88ba-4f96-92d4-c72024d48f81"),InfoTypeId = 2,Data = "e@e.com"},
                new Info{ContactId = Guid.Parse("19c45392-88ba-4f96-92d4-c72024d48f81"),InfoTypeId = 3,Data = "Bursa"},
        
            };
            
        public void Configure(EntityTypeBuilder<Info> builder)
        {   
            //Shadow properties
            builder.Property<DateTime>("Inserted").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
            builder.Property<DateTime>("Updated").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
            builder.Property(i => i.Data).IsRequired();
            //Relations
            builder.HasOne(i => i.InfoType);
            builder.HasOne(i => i.Contact);
            //Indexes
            builder.HasKey(i => new {i.ContactId, i.InfoTypeId});
            //Seeds
            builder.HasData(InfoSeed);
        }
    }
}
