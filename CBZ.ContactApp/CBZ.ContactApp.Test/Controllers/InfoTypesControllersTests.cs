using System.Linq;
using CBZ.ContactApp.Controllers;
using CBZ.ContactApp.Data.Configuration;
using CBZ.ContactApp.Data.Model;
using CBZ.ContactApp.Data.Repository;
using CBZ.ContactApp.Test.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            _fixture.PopulatePartial();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            ActionResult<IQueryable<InfoType>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            ActionResult<IQueryable<InfoType>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            _fixture.PopulatePartial();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            var e=InfoTypeEntityTypeConfiguration.InfoTypeSeed.First().Id;
            ActionResult<InfoType> result = controller.Get(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            var e=InfoTypeEntityTypeConfiguration.InfoTypeSeed.First().Id;
            ActionResult<InfoType> result = controller.Get(e);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            ActionResult<InfoType> result = controller.Post(InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(0));
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            _fixture.PopulateAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            ActionResult<InfoType> result = controller.Post(InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            _fixture.PopulateAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Name = "Gg";
            ActionResult<InfoType> result = controller.Put(eid,e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            var e = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(2);
            ActionResult<InfoType> result = controller.Put(e.Id,e);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            _fixture.PopulatePartial();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            ActionResult<InfoType> result = controller.Delete(eid);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfoTypesController>>().Object;
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            ActionResult<InfoType> result = controller.Delete(eid);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }


}
