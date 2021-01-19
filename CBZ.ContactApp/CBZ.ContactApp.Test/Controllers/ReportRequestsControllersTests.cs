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
    public class ReportRequestsControllersTests
    {
        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            fixture.PopulatePartial();
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            ActionResult<IQueryable<ReportRequest>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            ActionResult<IQueryable<ReportRequest>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            fixture.PopulatePartial();
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            var e=ReportRequestEntityTypeConfiguration.ReportRequestSeed.First().Id;
            ActionResult<ReportRequest> result = controller.Get(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            var e=ReportRequestEntityTypeConfiguration.ReportRequestSeed.First().Id;
            ActionResult<ReportRequest> result = controller.Get(e);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            ActionResult<ReportRequest> result = controller.Post(ReportRequestEntityTypeConfiguration.ReportRequestSeed.First());
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            fixture.PopulateAll();
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            ActionResult<ReportRequest> result = controller.Post(ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_badRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            fixture.PopulateAll();
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            var eid = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Location = "Gg";
            var delta = new Delta<ReportRequest>(typeof(ReportRequest));
            delta.TrySetPropertyValue(nameof(ReportRequest.Location), e.Location);
            ActionResult<ReportRequest> result = controller.Put(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            var e = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(0);
            var delta = new Delta<ReportRequest>(typeof(ReportRequest));
            delta.TrySetPropertyValue(nameof(ReportRequest.Location), e.Location);
            ActionResult<ReportRequest> result = controller.Put(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            fixture.PopulateAll();
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            var eid = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Location = "Gg";
            var delta = new Delta<ReportRequest>(typeof(ReportRequest));
            delta.TrySetPropertyValue(nameof(ReportRequest.Location), e.Location);
            ActionResult<ReportRequest> result = controller.Patch(e.Id,delta);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            var e = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(0);
            var delta = new Delta<ReportRequest>(typeof(ReportRequest));
            delta.TrySetPropertyValue(nameof(ReportRequest.Location), e.Location);
            ActionResult<ReportRequest> result = controller.Patch(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            fixture.PopulatePartial();
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            var eid = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(0).Id;
            ActionResult<ReportRequest> result = controller.Delete(eid);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportRequestsController>>().Object;
            var repository = new ReportRequestRepository(fixture.context);
            var controller = new ReportRequestsController(logger, repository);
            var eid = ReportRequestEntityTypeConfiguration.ReportRequestSeed.ElementAt(1).Id;
            ActionResult<ReportRequest> result = controller.Delete(eid);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }


}
