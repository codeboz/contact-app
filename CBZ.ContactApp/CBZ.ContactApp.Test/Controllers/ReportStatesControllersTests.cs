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
    public class ReportStatesControllersTests
    {
        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            fixture.PopulatePartial();
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            ActionResult<IQueryable<ReportState>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            ActionResult<IQueryable<ReportState>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            fixture.PopulatePartial();
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            var e=ReportStateEntityTypeConfiguration.ReportStateSeed.First().Id;
            ActionResult<ReportState> result = controller.Get(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            var e=ReportStateEntityTypeConfiguration.ReportStateSeed.First().Id;
            ActionResult<ReportState> result = controller.Get(e);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            var e = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(0);
            ActionResult<ReportState> result = controller.Post(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            fixture.PopulateAll();
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            ActionResult<ReportState> result = controller.Post(ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            fixture.PopulateAll();
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            var eid = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Name = "Gg";
            var delta = new Delta<ReportState>(typeof(ReportState));
            delta.TrySetPropertyValue(nameof(ReportState.Name),e.Name as object);
            ActionResult<ReportState> result = controller.Put(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            var e = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1);
            var delta = new Delta<ReportState>(typeof(ReportState));
            delta.TrySetPropertyValue(nameof(ReportState.Name),e.Name as object);
            ActionResult<ReportState> result = controller.Put(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            fixture.PopulateAll();
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            var eid = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.Name = "Gg";
            var delta = new Delta<ReportState>(typeof(ReportState));
            delta.TrySetPropertyValue(nameof(ReportState.Name),e.Name as object);
            ActionResult<ReportState> result = controller.Patch(e.Id,delta);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            var e = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1);
            var delta = new Delta<ReportState>(typeof(ReportState));
            delta.TrySetPropertyValue(nameof(ReportState.Name),e.Name as object);
            ActionResult<ReportState> result = controller.Patch(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            fixture.PopulatePartial();
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            var eid = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
            ActionResult<ReportState> result = controller.Delete(eid);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportStatesController>>().Object;
            var repository = new ReportStateRepository(fixture.context);
            var controller = new ReportStatesController(logger, repository);
            var eid = ReportStateEntityTypeConfiguration.ReportStateSeed.ElementAt(1).Id;
            ActionResult<ReportState> result = controller.Delete(eid);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }


}
