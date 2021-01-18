using System.Linq;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Repository;
using CBZ.ContactApp.Test.Fixtures;
using Xunit;

namespace CBZ.ContactApp.Test.Repository
{
    public class InfoTypeRepositoryTests:IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _fixture;

        public InfoTypeRepositoryTests()
        {
            _fixture = new DbContextFixture();
        }

        [Fact]
        public void InfoType_Count_When_All_Populated_Should_Be_Three()
        {
            _fixture.PopulateAll();
            var repository= new InfoTypeRepository(_fixture.context);
            var c = repository.Get().Count();
            Assert.Equal(3,c);
        }
        
        
        [Fact]
        public void Add_A_InfoType_When_Not_Populated_Should_Be_Four()
        {
            _fixture.PopulatePartial();
            var repository= new InfoTypeRepository(_fixture.context);
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
            _fixture.PopulateAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var id = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(2).Id;
            var entity = repository.Find(id as object).Result;
            repository.Remove(entity);
            var entities = repository.Get();
            var count = entities.Count();
            Assert.Equal(2,count);
        }

         [Fact]
         public void Find_And_Where_InfoType_Should_Be_Same()
         {
             _fixture.PopulateAll();
             var repository = new InfoTypeRepository(_fixture.context);
             var id = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(2).Id;
             var entityFind = repository.Find(id as object).Result;
             var entityWhere = repository.Where(e=>e.Id==id).First();
             Assert.Equal(entityFind,entityWhere);
         }

         [Fact]
         public void Update_the_Data_Of_InfoType_Should_Not_Be_Same()
         {
             _fixture.PopulateAll();
             var repository = new InfoTypeRepository(_fixture.context);
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
