using System.Linq;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Repository;
using CBZ.ContactApp.Test.Fixtures;
using Xunit;

namespace CBZ.ContactApp.Test.Repository
{
    public class ReportStateRepositoryTests
    {
        private readonly DbContextFixture _fixture;

        public ReportStateRepositoryTests()
        {
            _fixture = new DbContextFixture();
        }

        [Fact]
        public void ReportState_Count_When_All_Populated_Should_Be_Two()
        {
            _fixture.PopulateAll();
            var repository= new ReportStateRepository(_fixture.context);
            var c = repository.Get().Count();
            Assert.Equal(2,c);
        }

        [Fact]
        public void Add_A_ReportState_When_Not_Populated_Should_Be_Two()
        {
            
            var repository= new ReportStateRepository(_fixture.context);
            var entity = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1);
            entity.Id=3;
            entity.Name = "Tamamlandı";
            repository.Add(entity);
            var count = repository.Get().Count();
            Assert.Equal(1,count);
        }
        
        [Fact]
        public void Remove_A_ReportState_When_All_Populated_Should_Be_One()
        {
            _fixture.PopulateAll();
            var repository = new ReportStateRepository(_fixture.context);
            var id = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
            var entity = repository.Find(id as object).Result;
            repository.Remove(entity);
            var entities = repository.Get();
            var count = entities.Count();
            Assert.Equal(1,count);
        }

         [Fact]
         public void Find_And_Where_ReportState_Should_Be_Same()
         {
             _fixture.PopulateAll();
             var repository = new ReportStateRepository(_fixture.context);
             var id = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
             var entityFind = repository.Find(id as object).Result;
             var entityWhere = repository.Where(e=>e.Id==id).First();
             Assert.Equal(entityFind,entityWhere);
         }

         [Fact]
         public void Update_the_Data_Of_ReportState_Should_Not_Be_Same()
         {
             _fixture.PopulateAll();
             var repository = new ReportStateRepository(_fixture.context);
             var id = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
             var data = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Name;
             var entity = repository.Find(id as object).Result;
             entity.Name = "Fa";
             repository.Update(entity);
             var entityUpdated =  repository.Find(id as object).Result;
             Assert.NotEqual( data,entityUpdated.Name);
         }
     }
}
