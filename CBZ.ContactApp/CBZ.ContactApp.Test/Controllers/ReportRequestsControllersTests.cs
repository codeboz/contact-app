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
    public class ReportRequestsControllersTests
    {
        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            ActionResult<IQueryable<ReportRequest>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            ActionResult<IQueryable<ReportRequest>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            var e=ReportRequestEntityTypeConfiguration.ReportRequestSeed.First().Id;
            ActionResult<ReportRequest> result = controller.Get(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            var e=ReportRequestEntityTypeConfiguration.ReportRequestSeed.First().Id;
            ActionResult<ReportRequest> result = controller.Get(e);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            ActionResult<ReportRequest> result = controller.Post(ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(0));
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            _fixture.PopulateAll();
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            ActionResult<ReportRequest> result = controller.Post(ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            _fixture.PopulateAll();
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            var eid = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Location = "Gg";
            ActionResult<ReportRequest> result = controller.Put(eid,e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            var e = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(0);
            ActionResult<ReportRequest> result = controller.Put(e.Id,e);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            var eid = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(0).Id;
            ActionResult<ReportRequest> result = controller.Delete(eid);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(_fixture.context);
            var controller = new ReportRequestsController(_logger, repository);
            var eid = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1).Id;
            ActionResult<ReportRequest> result = controller.Delete(eid);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }


}
