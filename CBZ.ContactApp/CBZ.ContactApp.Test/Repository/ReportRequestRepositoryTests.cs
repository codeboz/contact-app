using System;
using System.Linq;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Repository;
using CBZ.ContactApp.Test.Fixtures;
using Xunit;

namespace CBZ.ContactApp.Test.Repository
{
    public class ReportRequestRepositoryTests
    {
        private readonly DbContextFixture fixture;

        public ReportRequestRepositoryTests()
        {
            fixture = new DbContextFixture();
        }

        [Fact]
        public void ReportRequest_Count_When_All_Populated_Should_Be_Two()
        {
            fixture.PopulateAll();
            var repository= new ReportRequestRepository(fixture.context);
            var c = repository.Get().Count();
            Assert.Equal(2,c);
        }

        [Fact]
        public void Add_A_ReportRequest_When_Partial_Populated_Should_Be_Two()
        {
            fixture.PopulatePartial();
            var repository= new ReportRequestRepository(fixture.context);
            var entity = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1);
            entity.Id=Guid.NewGuid();
            entity.ReportStateId = 1;
            repository.Add(entity);
            var count = repository.Get().Count();
            Assert.Equal(2,count);
        }
        
        [Fact]
        public void Remove_A_ReportRequest_When_All_Populated_Should_Be_One()
        {
            fixture.PopulateAll();
            var repository = new ReportRequestRepository(fixture.context);
            var id = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1).Id;
            var entity = repository.Find(id as object).Result;
            repository.Remove(entity);
            var entities = repository.Get();
            var count = entities.Count();
            Assert.Equal(1,count);
        }
        
         [Fact]
         public void Update_the_Data_Of_ReportRequest_Should_Not_Be_Same()
         {
             fixture.PopulateAll();
             var repository = new ReportRequestRepository(fixture.context);
             var id = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1).Id;
             var data = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1).Location;
             var entity = repository.Find(id as object).Result;
             entity.Location = "F";
             repository.Update(entity);
             var entityUpdated =  repository.Find(id as object).Result;
             Assert.NotEqual(data,entityUpdated.Location);
         }
     }
}
