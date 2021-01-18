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
    public class InfosControllersTests
    {
        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            _fixture.PopulatePartial();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            ActionResult<IQueryable<Info>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            ActionResult<IQueryable<Info>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            _fixture.PopulatePartial();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            var e=InfoEntityTypeConfiguration.InfoSeed.First();
            ActionResult<Info> result = controller.Get(e.ContactId,e.InfoTypeId);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            var e=InfoEntityTypeConfiguration.InfoSeed.First();
            ActionResult<Info> result = controller.Get(e.ContactId,e.InfoTypeId);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            _fixture.PopulatePartial();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            ActionResult<Info> result = controller.Post(InfoEntityTypeConfiguration.InfoSeed.ElementAt(2));
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            _fixture.PopulateAll();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            ActionResult<Info> result = controller.Post(InfoEntityTypeConfiguration.InfoSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            _fixture.PopulateAll();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            var e = repository.Find(entity.ContactId as object,entity.InfoTypeId as object).Result;
            e.Data = "Gg";
            ActionResult<Info> result = controller.Put(e.ContactId,e.InfoTypeId,e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            var e = InfoEntityTypeConfiguration.InfoSeed.ElementAt(2);
            ActionResult<Info> result = controller.Put(e.ContactId,e.InfoTypeId,e);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            _fixture.PopulatePartial();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            var e = repository.Find(entity.ContactId as object,entity.InfoTypeId as object).Result;
            ActionResult<Info> result = controller.Delete(e.ContactId,e.InfoTypeId);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<InfosController>>().Object;
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            ActionResult<Info> result = controller.Delete(entity.ContactId,entity.InfoTypeId);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
