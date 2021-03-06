using System.Linq;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Repository;
using CBZ.ContactApp.Test.Fixtures;
using Xunit;

namespace CBZ.ContactApp.Test.Repository
{
    public class InfoTypeRepositoryTests
    {
        private readonly DbContextFixture fixture;

        public InfoTypeRepositoryTests()
        {
            fixture = new DbContextFixture();
        }

        [Fact]
        public void InfoType_Count_When_All_Populated_Should_Be_Three()
        {
            fixture.PopulateAll();
            var repository= new InfoTypeRepository(fixture.context);
            var c = repository.Get().Count();
            Assert.Equal(3,c);
        }
        
        
        [Fact]
        public void Add_A_InfoType_When_Not_Populated_Should_Be_Four()
        {
            fixture.PopulatePartial();
            var repository= new InfoTypeRepository(fixture.context);
            var entity = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1);
            entity.Id = 4;
            entity.Name = "TestInfoType";
            repository.Add(entity);
            var count = repository.Get().Count();
            Assert.Equal(4,count);
        }
        
        [Fact]
        public void Remove_A_InfoType_When_All_Populated_Should_Be_Two()
        {
            fixture.PopulateAll();
            var repository = new InfoTypeRepository(fixture.context);
            var id = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(2).Id;
            var entity = repository.Find(id as object).Result;
            repository.Remove(entity);
            var entities = repository.Get();
            var count = entities.Count();
            Assert.Equal(2,count);
        }

        
         [Fact]
         public void Update_the_Data_Of_InfoType_Should_Not_Be_Same()
         {
             fixture.PopulateAll();
             var repository = new InfoTypeRepository(fixture.context);
             var id = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(2).Id;
             var data = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(2).Name;
             var entity = repository.Find(id as object).Result;
             entity.Name = "F";
             repository.Update(entity);
             var entityUpdated =  repository.Find(id as object).Result;
             Assert.NotEqual(data,entityUpdated.Name);
         }
     }
}
