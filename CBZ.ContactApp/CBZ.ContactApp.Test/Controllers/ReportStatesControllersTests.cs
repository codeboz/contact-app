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
    public class ReportStatesControllersTests
    {
        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            ActionResult<IQueryable<ReportState>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            ActionResult<IQueryable<ReportState>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            var e=ReportStateEntityTypeConfiguration.ReportStateSeed.First().Id;
            ActionResult<ReportState> result = controller.Get(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            var e=ReportStateEntityTypeConfiguration.ReportStateSeed.First().Id;
            ActionResult<ReportState> result = controller.Get(e);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            var e = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(0);
            ActionResult<ReportState> result = controller.Post(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            _fixture.PopulateAll();
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            ActionResult<ReportState> result = controller.Post(ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            _fixture.PopulateAll();
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            var eid = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Name = "Gg";
            ActionResult<ReportState> result = controller.Put(e.Id,e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            var e = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1);
            ActionResult<ReportState> result = controller.Put(e.Id,e);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            var eid = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
            ActionResult<ReportState> result = controller.Delete(eid);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(_fixture.context);
            var controller = new ReportStatesController(_logger, repository);
            var eid = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
            ActionResult<ReportState> result = controller.Delete(eid);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }


}
