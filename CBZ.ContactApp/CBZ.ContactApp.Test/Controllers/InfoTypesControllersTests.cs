using System.Linq;
using CBZ.ContactApp.Controllers;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Model;
using CBZ.ContactApp.Data.Repository;
using CBZ.ContactApp.Test.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter.Value;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CBZ.ContactApp.Test.Controllers
{
    public class InfoTypesControllersTests
    {
        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            fixture.PopulatePartial();
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            ActionResult<IQueryable<InfoType>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            ActionResult<IQueryable<InfoType>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            fixture.PopulatePartial();
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            var e=InfoTypeEntityTypeConfiguration.InfoTypeSeed.First().Id;
            ActionResult<InfoType> result = controller.Get(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            var e=InfoTypeEntityTypeConfiguration.InfoTypeSeed.First().Id;
            ActionResult<InfoType> result = controller.Get(e);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            ActionResult<InfoType> result = controller.Post(InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(0));
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            fixture.PopulateAll();
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            ActionResult<InfoType> result = controller.Post(InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_badRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            fixture.PopulateAll();
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Name = "Gg";
            var delta = new Delta<InfoType>(typeof(InfoType));
            delta.TrySetPropertyValue(nameof(InfoType.Name), e.Name);
            ActionResult<InfoType> result = controller.Put(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            var e = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(2);
            var delta = new Delta<InfoType>(typeof(InfoType));
            delta.TrySetPropertyValue(nameof(InfoType.Name), e.Name);
            ActionResult<InfoType> result = controller.Put(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            fixture.PopulateAll();
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Name = "Gg";
            var delta = new Delta<InfoType>(typeof(InfoType));
            delta.TrySetPropertyValue(nameof(InfoType.Name), e.Name);
            ActionResult<InfoType> result = controller.Patch(e.Id,delta);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            var e = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(2);
            var delta = new Delta<InfoType>(typeof(InfoType));
            delta.TrySetPropertyValue(nameof(InfoType.Name), e.Name);
            ActionResult<InfoType> result = controller.Patch(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            fixture.PopulatePartial();
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            ActionResult<InfoType> result = controller.Delete(eid);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(fixture.context);
            var controller = new InfoTypesController(logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            ActionResult<InfoType> result = controller.Delete(eid);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }


}
