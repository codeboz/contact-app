using System;
using System.Linq;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Test.Fixtures
{
    public class DbContextFixture: IDisposable
    {
        public ContactDbContext context;
        public DbContextFixture()
        {
            FreshCreate();
        }

        private void FreshCreate()
        {
            Dispose();
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase("ContactDatabase")
                .Options;
            context = new ContactDbContext(options);
        }

        public void PopulatePartial()
        {
            FreshCreate();
            context.Contacts.AddRangeAsync(ContactEntityTypeConfiguration.ContactSeed.Take(1));
            context.InfoTypes.AddRangeAsync(InfoTypeEntityTypeConfiguration.InfoTypeSeed);
            context.Infos.AddRangeAsync(InfoEntityTypeConfiguration.InfoSeed.Take(2));
            context.ReportStates.AddRangeAsync(ReportStateEntityTypeConfiguration.ReportStateSeed);
            context.ReportRequests.AddRangeAsync(ReportRequestEntityTypeConfiguration.ReportRequestSeed.Take(1));
            context.Reports.AddRangeAsync(ReportEntityTypeConfiguration.ReportSeed.Take(1));
            context.SaveChanges();
        }
        
        public void PopulateAll()
        {
            FreshCreate();
            context.Contacts.AddRangeAsync(ContactEntityTypeConfiguration.ContactSeed);
            context.InfoTypes.AddRangeAsync(InfoTypeEntityTypeConfiguration.InfoTypeSeed);
            context.Infos.AddRangeAsync(InfoEntityTypeConfiguration.InfoSeed);
            context.ReportStates.AddRangeAsync(ReportStateEntityTypeConfiguration.ReportStateSeed);
            context.ReportRequests.AddRangeAsync(ReportRequestEntityTypeConfiguration.ReportRequestSeed);
            context.Reports.AddRangeAsync(ReportEntityTypeConfiguration.ReportSeed);
            context.SaveChanges();
        }

        public void PruneAll()
        {
            FreshCreate();
            context.Contacts.RemoveRange(context.Contacts.AsQueryable());
            context.Infos.RemoveRange(context.Infos.AsQueryable());
            context.InfoTypes.RemoveRange(context.InfoTypes.AsQueryable());
            context.ReportRequests.RemoveRange(context.ReportRequests.AsQueryable());
            context.ReportStates.RemoveRange(context.ReportStates.AsQueryable());
            context.Reports.RemoveRange(context.Reports.AsQueryable());
            context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (context == null)
            {
                return;
            }
            context.Database.EnsureDeleted();
            context.SaveChangesAsync();
            context.Dispose();
        }
    }
}
