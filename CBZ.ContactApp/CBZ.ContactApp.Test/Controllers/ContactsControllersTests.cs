﻿using System;
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
    public class ContactsControllersTests
    {
        [Fact]
        public void Controller_Get_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            ActionResult<IQueryable<Contact>> result = controller.Get();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            ActionResult<IQueryable<Contact>> result = controller.Get();
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            ActionResult<Contact> result = controller.Get(ContactEntityTypeConfiguration.ContactSeed.First().Id);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ById_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            ActionResult<Contact> result = controller.Get(Guid.NewGuid());
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Get_ByNameSurname_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            var name = ContactEntityTypeConfiguration.ContactSeed.First().Name;
            var surname = ContactEntityTypeConfiguration.ContactSeed.First().Surname;
            ActionResult<Contact> result = controller.ByNameSurname(name, surname);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Get_ByNameSurname_Should_Be_NoContentResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            var name = ContactEntityTypeConfiguration.ContactSeed.First().Name;
            var surname = ContactEntityTypeConfiguration.ContactSeed.First().Surname;
            ActionResult<Contact> result = controller.ByNameSurname(name, surname);
            result.Result.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            ActionResult<Contact> result = controller.Post(ContactEntityTypeConfiguration.ContactSeed.ElementAt(1));
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Post_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            _fixture.PopulateAll();
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            ActionResult<Contact> result = controller.Post(ContactEntityTypeConfiguration.ContactSeed.ElementAt(1));
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            _fixture.PopulateAll();
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            var id = ContactEntityTypeConfiguration.ContactSeed.ElementAt(2).Id;
            var e = repository.Find(id as object).Result;
            e.Name = "Gg";
            ActionResult<Contact> result = controller.Put(e.Id,e);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Put_Should_Be_BadRequest()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            var e = ContactEntityTypeConfiguration.ContactSeed.ElementAt(2);
            ActionResult<Contact> result = controller.Put(e.Id,e);
            result.Result.Should().BeOfType<BadRequestResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_OkResult()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            _fixture.PopulatePartial();
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            var e = ContactEntityTypeConfiguration.ContactSeed.ElementAt(0);
            repository.Find(e.Id as object);
            ActionResult<Contact> result = controller.Delete(e.Id);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public void Controller_Delete_Should_Be_NotFound()
        {
            var _fixture = new DbContextFixture();
            var _logger = new Mock<ILogger<ContactsController>>().Object;
            var repository = new ContactRepository(_fixture.context);
            var controller = new ContactsController(_logger, repository);
            var e = ContactEntityTypeConfiguration.ContactSeed.ElementAt(2);
            ActionResult<Contact> result = controller.Delete(e.Id);
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
