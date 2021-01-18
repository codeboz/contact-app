using CBZ.ContactApp.Controllers;
using CBZ.ContactApp.Data.Model;
using CBZ.ContactApp.Data.Repository;
using CBZ.ContactApp.Test.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CBZ.ContactApp.Test.Controllers
{
    public class ContactsControllersTests:IClassFixture<DbContextFixture>
    {
        private readonly ContactsController _controller;
        
        public ContactsControllersTests()
        {
            var fixture = new DbContextFixture();
            IRepository<Contact> repository = new ContactRepository(fixture.context);
            var loggerMock = new Mock<ILogger<ContactsController>>();
            var logger = loggerMock.Object;
            _controller = new ContactsController(fixture.context, logger, repository);
        }

        [Fact]
        public void Contact_Count_When_All_Populated_Should_Be_Fifteen()
        {
            
        }
        
    }
}
