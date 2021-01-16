using System;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InfoTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InfoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReportStateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReportRequestEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}