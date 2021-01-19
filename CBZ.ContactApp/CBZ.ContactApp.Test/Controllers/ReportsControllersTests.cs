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
    public class ReportsControllersTests
    {
        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            fixture.PopulatePartial();
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            ActionResult<IQueryable<Report>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            ActionResult<IQueryable<Report>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            fixture.PopulatePartial();
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            var e=ReportEntityTypeConfiguration.ReportSeed.First().Id;
            ActionResult<Report> result = controller.Get(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            var e=ReportEntityTypeConfiguration.ReportSeed.First().Id;
            ActionResult<Report> result = controller.Get(e);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            var e = ReportEntityTypeConfiguration.ReportSeed.ElementAt(0);
            ActionResult<Report> result = controller.Post(e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            fixture.PopulateAll();
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            ActionResult<Report> result = controller.Post(ReportEntityTypeConfiguration.ReportSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_badRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            fixture.PopulateAll();
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            var eid = ReportEntityTypeConfiguration.ReportSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.ContactCount = 4;
            var delta = new Delta<Report>(typeof(Report));
            delta.TrySetPropertyValue(nameof(Report.ContactCount),e.ContactCount as object);
            ActionResult<Report> result = controller.Put(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            var e = ReportEntityTypeConfiguration.ReportSeed.ElementAt(1);
            var delta = new Delta<Report>(typeof(Report));
            delta.TrySetPropertyValue(nameof(Report.Location),e.Location);
            ActionResult<Report> result = controller.Put(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            fixture.PopulateAll();
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            var eid = ReportEntityTypeConfiguration.ReportSeed.ElementAt(1).Id;
            var e = repository.Find(eid as object).Result;
            e.ContactCount = 4;
            var delta = new Delta<Report>(typeof(Report));
            delta.TrySetPropertyValue(nameof(Report.ContactCount),e.ContactCount as object);
            ActionResult<Report> result = controller.Patch(e.Id,delta);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            var e = ReportEntityTypeConfiguration.ReportSeed.ElementAt(1);
            var delta = new Delta<Report>(typeof(Report));
            delta.TrySetPropertyValue(nameof(Report.Location),e.Location);
            ActionResult<Report> result = controller.Patch(e.Id,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            fixture.PopulatePartial();
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            var eid = ReportEntityTypeConfiguration.ReportSeed.ElementAt(0).Id;
            ActionResult<Report> result = controller.Delete(eid);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<ReportsController>>().Object;
            var repository = new ReportRepository(fixture.context);
            var controller = new ReportsController(logger, repository);
            var eid = ReportEntityTypeConfiguration.ReportSeed.ElementAt(1).Id;
            ActionResult<Report> result = controller.Delete(eid);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
