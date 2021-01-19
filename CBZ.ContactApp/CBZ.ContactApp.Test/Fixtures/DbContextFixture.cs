using System;
using System.Linq;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CBZ.ContactApp.Test.Fixtures
{
    public class DbContextFixture
    {
        public ContactDbContext context;
        public DbContextFixture()
        {
            Random rnd = new Random();
            rnd.Next().ToString();
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options;
            context = new ContactDbContext(options);
        }

        public void PopulatePartial()
        {
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
            context.Contacts.AddRangeAsync(ContactEntityTypeConfiguration.ContactSeed);
            context.InfoTypes.AddRangeAsync(InfoTypeEntityTypeConfiguration.InfoTypeSeed);
            context.Infos.AddRangeAsync(InfoEntityTypeConfiguration.InfoSeed);
            context.ReportStates.AddRangeAsync(ReportStateEntityTypeConfiguration.ReportStateSeed);
            context.ReportRequests.AddRangeAsync(ReportRequestEntityTypeConfiguration.ReportRequestSeed);
            context.Reports.AddRangeAsync(ReportEntityTypeConfiguration.ReportSeed);
            context.SaveChanges();
        }
    }
}
