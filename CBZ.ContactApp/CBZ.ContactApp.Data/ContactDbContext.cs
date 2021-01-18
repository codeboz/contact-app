using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Data
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<InfoType> InfoTypes { get; set; }
        public DbSet<ReportRequest> ReportRequests { get; set; }
        public DbSet<ReportState> ReportStates { get; set; }
        
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