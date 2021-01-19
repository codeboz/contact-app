﻿using System.Linq;
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
    public class InfosControllersTests
    {
        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            fixture.PopulatePartial();
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            ActionResult<IQueryable<Info>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            ActionResult<IQueryable<Info>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            fixture.PopulatePartial();
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            var e=InfoEntityTypeConfiguration.InfoSeed.First();
            ActionResult<Info> result = controller.Get(e.ContactId,e.InfoTypeId);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            var e=InfoEntityTypeConfiguration.InfoSeed.First();
            ActionResult<Info> result = controller.Get(e.ContactId,e.InfoTypeId);
            result.Result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            fixture.PopulatePartial();
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            ActionResult<Info> result = controller.Post(InfoEntityTypeConfiguration.InfoSeed.ElementAt(2));
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            fixture.PopulateAll();
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            ActionResult<Info> result = controller.Post(InfoEntityTypeConfiguration.InfoSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_badRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            fixture.PopulateAll();
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            var e = repository.Find(entity.ContactId as object,entity.InfoTypeId as object).Result;
            e.Data = "Gg";
            var delta = new Delta<Info>(typeof(Info));
            delta.TrySetPropertyValue(nameof(Info.Data), e.Data);
            ActionResult<Info> result = controller.Put(e.ContactId,e.InfoTypeId,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            var e = InfoEntityTypeConfiguration.InfoSeed.ElementAt(2);
            var delta = new Delta<Info>(typeof(Info));
            delta.TrySetPropertyValue(nameof(Info.Data), e.Data);
            ActionResult<Info> result = controller.Put(e.ContactId,e.InfoTypeId,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            fixture.PopulateAll();
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            var e = repository.Find(entity.ContactId as object,entity.InfoTypeId as object).Result;
            e.Data = "Gg";
            var delta = new Delta<Info>(typeof(Info));
            delta.TrySetPropertyValue(nameof(Info.Data), e.Data);
            ActionResult<Info> result = controller.Patch(e.ContactId,e.InfoTypeId,delta);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Patch_Should_Be_BadRequest()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            var e = InfoEntityTypeConfiguration.InfoSeed.ElementAt(2);
            var delta = new Delta<Info>(typeof(Info));
            delta.TrySetPropertyValue(nameof(Info.Data), e.Data);
            ActionResult<Info> result = controller.Patch(e.ContactId,e.InfoTypeId,delta);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            fixture.PopulatePartial();
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            var e = repository.Find(entity.ContactId as object,entity.InfoTypeId as object).Result;
            ActionResult<Info> result = controller.Delete(e.ContactId,e.InfoTypeId);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var fixture = new DbContextFixture();
            var logger = new Mock<ILogger<InfosController>>().Object;
            var repository = new InfoRepository(fixture.context);
            var controller = new InfosController(logger, repository);
            var entity = InfoEntityTypeConfiguration.InfoSeed.ElementAt(1);
            ActionResult<Info> result = controller.Delete(entity.ContactId,entity.InfoTypeId);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
