using System.Linq;
using CBZ.ContactApp.Controllers;
using CBZ.ContactApp.Data;
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
    public class InfoTypesControllersTests:IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _fixture;
        private readonly ILogger<InfoTypesController> _logger;

        public InfoTypesControllersTests(DbContextFixture fixture)
        {
            _fixture = fixture;
            var loggerMock = new Mock<ILogger<InfoTypesController>>();
            _logger = loggerMock.Object;
        }

        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            _fixture.PopulatePartial();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            ActionResult<IQueryable<InfoType>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            _fixture.PruneAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            ActionResult<IQueryable<InfoType>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            _fixture.PopulatePartial();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            var e=InfoTypeEntityTypeConfiguration.InfoTypeSeed.First().Id;
            ActionResult<InfoType> result = controller.Get(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            _fixture.PruneAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            var e=InfoTypeEntityTypeConfiguration.InfoTypeSeed.First().Id;
            ActionResult<InfoType> result = controller.Get(e);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            _fixture.PruneAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            ActionResult<InfoType> result = controller.Post(InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(0));
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            _fixture.PopulateAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            ActionResult<InfoType> result = controller.Post(InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_OkResult()
        {
            _fixture.PopulateAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Name = "Gg";
            ActionResult<InfoType> result = controller.Put(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            _fixture.PruneAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            ActionResult<InfoType> result = controller.Put(InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(2));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            _fixture.PopulatePartial();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            ActionResult<InfoType> result = controller.Delete(eid);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            _fixture.PruneAll();
            var repository = new InfoTypeRepository(_fixture.context);
            var controller = new InfoTypesController(_fixture.context, _logger, repository);
            var eid = InfoTypeEntityTypeConfiguration.InfoTypeSeed.ElementAt(1).Id;
            ActionResult<InfoType> result = controller.Delete(eid);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }


}
