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
    public class InfosControllersTests:IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _fixture;
        private readonly ILogger<InfosController> _logger;

        public InfosControllersTests(DbContextFixture fixture)
        {
            _fixture = fixture;
            var loggerMock = new Mock<ILogger<InfosController>>();
            _logger = loggerMock.Object;
        }

        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            _fixture.PopulatePartial();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            ActionResult<IQueryable<Info>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            _fixture.PruneAll();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            ActionResult<IQueryable<Info>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            _fixture.PopulatePartial();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            var e=InfoEntityTypeConfiguration.InfoSeed.First();
            ActionResult<Info> result = controller.Get(e.ContactId,e.InfoTypeId);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            _fixture.PruneAll();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            var e=InfoEntityTypeConfiguration.InfoSeed.First();
            ActionResult<Info> result = controller.Get(e.ContactId,e.InfoTypeId);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            _fixture.PopulatePartial();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            ActionResult<Info> result = controller.Post(InfoEntityTypeConfiguration.InfoSeed.ElementAt(2));
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            _fixture.PopulateAll();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            ActionResult<Info> result = controller.Post(InfoEntityTypeConfiguration.InfoSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_OkResult()
        {
            _fixture.PopulateAll();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            var e = repository.Find(entity.ContactId as object,entity.InfoTypeId as object).Result;
            e.Data = "Gg";
            ActionResult<Info> result = controller.Put(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            _fixture.PruneAll();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            ActionResult<Info> result = controller.Put(InfoEntityTypeConfiguration.InfoSeed.ElementAt(2));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            _fixture.PopulatePartial();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            var e = repository.Find(entity.ContactId as object,entity.InfoTypeId as object).Result;
            ActionResult<Info> result = controller.Delete(e.ContactId,e.InfoTypeId);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            _fixture.PruneAll();
            var repository = new InfoRepository(_fixture.context);
            var controller = new InfosController(_fixture.context, _logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            ActionResult<Info> result = controller.Delete(entity.ContactId,entity.InfoTypeId);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
