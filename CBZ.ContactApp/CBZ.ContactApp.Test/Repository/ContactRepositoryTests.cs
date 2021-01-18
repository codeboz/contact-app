using System.Linq;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Repository;
using Xunit;

namespace CBZ.ContactApp.Test.Repository
{
    public class ContactRepositoryTests:IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _fixture;

        public ContactRepositoryTests()
        {
            _fixture = new DbContextFixture();
        }

        [Fact]
        public void Contact_Count_When_All_Populated_Should_Be_Five()
        {
            _fixture.PopulateAll();
            var repository = new ContactRepository(_fixture.context);
                var contactCount = repository.Get().Count();
                Assert.Equal(5,contactCount);
        }
        
        [Fact]
        public void Contact_Count_When_Partial_Populated_Should_Be_One()
        {
            _fixture.PopulatePartial();
            var repository = new ContactRepository(_fixture.context);
            var contactCount = repository.Get().Count();
            Assert.Equal(1,contactCount);
        }
        
        [Fact]
        public void Add_A_Contact_When_Partial_Populated_Should_Be_Two()
        {
            _fixture.PopulatePartial();
            var repository = new ContactRepository(_fixture.context);
            repository.Add(ContactEntityTypeConfiguration.ContactSeed.ElementAt(2));
            var contactCount = repository.Get().Count();
            Assert.Equal(2,contactCount);
        }
        
        [Fact]
        public void Remove_A_Contact_When_All_Populated_Should_Be_Four()
        {
            _fixture.PopulateAll();
            var repository = new ContactRepository(_fixture.context);
            var id = ContactEntityTypeConfiguration.ContactSeed.ElementAt(2).Id;
            var entity = repository.Find(id as object).Result;
            repository.Remove(entity);
            var entities = repository.Get();
            var count = entities.Count();
            Assert.Equal(4,count);
        }
        
        [Fact]
        public void Find_And_Where_Contact_Should_Be_Same()
        {
            _fixture.PopulateAll();
            var repository = new ContactRepository(_fixture.context);
            var entityInsertedId = ContactEntityTypeConfiguration.ContactSeed.ElementAt(2).Id;
            var entityFind = repository.Find(entityInsertedId as object).Result;
            var entityWhere = repository.Where(e=>e.Id==entityInsertedId).First();
            Assert.Equal(entityFind,entityWhere);
        }
        
        [Fact]
        public void Update_the_Name_Of_Contact_Should_Not_Be_Same()
        {
            _fixture.PopulateAll();
            var repository = new ContactRepository(_fixture.context);
            var entityInsertedId = ContactEntityTypeConfiguration.ContactSeed.ElementAt(3).Id;
            var entityInsertedName = ContactEntityTypeConfiguration.ContactSeed.ElementAt(3).Name;
            var entity = repository.Find(entityInsertedId as object).Result;
            entity.Name = "F";
            repository.Update(entity);
            var entityUpdated = repository.Find(entityInsertedId as object).Result;
            Assert.NotEqual(entityInsertedName,entityUpdated.Name);
        }
    }
}
