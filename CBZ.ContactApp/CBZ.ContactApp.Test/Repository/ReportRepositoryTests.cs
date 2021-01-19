using System.Linq;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Repository;
using CBZ.ContactApp.Test.Fixtures;
using Xunit;

namespace CBZ.ContactApp.Test.Repository
{
    public class ReportRepositoryTests
    {
        private readonly DbContextFixture fixture;

        public ReportRepositoryTests()
        {
            fixture = new DbContextFixture();
        }

        [Fact]
        public void Report_Count_When_All_Populated_Should_Be_Two()
        {
            fixture.PopulateAll();
            var repository= new ReportRepository(fixture.context);
            var c = repository.Get().Count();
            Assert.Equal(2,c);
        }

        [Fact]
        public void Add_A_Report_When_Not_Populated_Should_Be_One()
        {
            var repository= new ReportRepository(fixture.context);
            var entity = ReportEntityTypeConfiguration.ReportSeed.ElementAt(1);
            entity.Id=3;
            repository.Add(entity);
            var count = repository.Get().Count();
            Assert.Equal(1,count);
        }
        
        [Fact]
        public void Remove_A_Report_When_All_Populated_Should_Be_One()
        {
            fixture.PopulateAll();
            var repository = new ReportRepository(fixture.context);
            var id = ReportEntityTypeConfiguration.ReportSeed.ElementAt(1).Id;
            var entity = repository.Find(id as object).Result;
            repository.Remove(entity);
            var entities = repository.Get();
            var count = entities.Count();
            Assert.Equal(1,count);
        }


         [Fact]
         public void Update_the_Data_Of_Report_Should_Not_Be_Same()
         {
             fixture.PopulateAll();
             var repository = new ReportRepository(fixture.context);
             var id = ReportEntityTypeConfiguration.ReportSeed.ElementAt(1).Id;
             var data = ReportEntityTypeConfiguration.ReportSeed.ElementAt(1).Location;
             var entity = repository.Find(id as object).Result;
             entity.Location = "Fa";
             repository.Update(entity);
             var entityUpdated =  repository.Find(id as object).Result;
             Assert.NotEqual( data,entityUpdated.Location);
         }
     }
}
