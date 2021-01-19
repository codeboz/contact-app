using System.Linq;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Repository;
using CBZ.ContactApp.Test.Fixtures;
using Xunit;

namespace CBZ.ContactApp.Test.Repository
{
    public class InfoRepositoryTests
    {
        private readonly DbContextFixture fixture;

        public InfoRepositoryTests()
        {
            fixture = new DbContextFixture();
        }

        [Fact]
        public void Info_Count_When_All_Populated_Should_Be_Fifteen()
        {
            fixture.PopulateAll();
            var repository= new InfoRepository(fixture.context);
            var c = repository.Get().Count();
            Assert.Equal(15,c);
        }
        
        [Fact]
        public void Info_Count_When_Partial_Populated_Should_Be_Two()
        {
            fixture.PopulatePartial();
            var repository= new InfoRepository(fixture.context);
            var contactCount = repository.Get().Count();
            Assert.Equal(2,contactCount);
        }
        
        [Fact]
        public void Add_A_Info_When_Partial_Populated_Should_Be_Three()
        {
            fixture.PopulatePartial();
            var repository= new InfoRepository(fixture.context);
            repository.Add(InfoEntityTypeConfiguration.InfoSeed.ElementAt(3));
            var count = repository.Get().Count();
            Assert.Equal(3,count);
        }
        
        [Fact]
        public void Remove_A_Info_When_All_Populated_Should_Be_FourTeen()
        {
            fixture.PopulateAll();
            var repository = new InfoRepository(fixture.context);
            var id = InfoEntityTypeConfiguration.InfoSeed.ElementAt(2).ContactId;
            var InfoTypeId = InfoEntityTypeConfiguration.InfoSeed.ElementAt(2).InfoTypeId;
            var entity = repository.Find(id as object,InfoTypeId as object).Result;
            repository.Remove(entity);
            var entities = repository.Get();
            var count = entities.Count();
            Assert.Equal(14,count);
        }
        
        [Fact]
        public void Update_the_Data_Of_Info_Should_Not_Be_Same()
        {
            fixture.PopulateAll();
            var repository = new InfoRepository(fixture.context);
            var contactId = InfoEntityTypeConfiguration.InfoSeed.ElementAt(2).ContactId;
            var infoTypeId = InfoEntityTypeConfiguration.InfoSeed.ElementAt(2).InfoTypeId;
            var data = InfoEntityTypeConfiguration.InfoSeed.ElementAt(2).Data;
            var entity = repository.Find(contactId as object,infoTypeId as object).Result;
            entity.Data = "F";
            repository.Update(entity);
            var entityUpdated =  repository.Find(contactId as object,infoTypeId as object).Result;
            Assert.NotEqual(data,entityUpdated.Data);
        }
    }
}
