using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using CBZ.ContactApp.Data.Model;
using CBZ.ContactApp.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CBZ.ContactApp.Controllers
{
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ContactsController:ODataController
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IRepository<Contact> _contactRepository;

        public ContactsController(ILogger<ContactsController> logger,IRepository<Contact> contactRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _contactRepository = contactRepository;
        }
        
        public ActionResult<IQueryable<Contact>> Get()
        {
            try
            {
                var c=_contactRepository.Get();
                return !c.Any() ? (ActionResult<IQueryable<Contact>>)NoContent() : Ok(c);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Contacts Error");
            }
            return NotFound();
        }
        
        public ActionResult<Contact> Get(Guid key)
        {
            try
            {
                var c = _contactRepository.Find(key as object);
                if (c.Exception!=null) throw c.Exception;
                return c.Result==null ?  (ActionResult<Contact>)NoContent() : Ok(c.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Contacts Error");
            }
            return NoContent();
        }
        
        [HttpGet]
        public ActionResult<Contact> ByNameSurname(string name,string surname)
        {
            try
            {
                var contacts = _contactRepository.Where(i=>i.Name==name && i.Surname==surname).FirstOrDefault();
                return contacts==null ? (ActionResult<Contact>)NoContent():Ok(contacts);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Contact.ByNameSurname");
            }
            return NoContent();
        }

        public ActionResult<Contact> Post([FromBody]Contact contact)
        {
            try
            {
                var c=_contactRepository.Add(contact);
                if (c.Exception != null) throw c.Exception;
                return c.Result == null ? (ActionResult<Contact>) BadRequest() : Ok(c.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Contact creation problem");
            }

            return BadRequest();
        }
        
        public ActionResult<Contact> Put(Guid key,[FromBody]Contact contact)
        {
            try
            {
                var cdb = _contactRepository.Find(key as object).Result;
                if (cdb.Id == contact.Id)
                {
                    var c = _contactRepository.Update(contact);
                    if (c.Exception != null) throw c.Exception;
                    return c.Result == null ? BadRequest() : Ok(c.Result);
                }
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }

            return BadRequest();
        }
        
        public ActionResult<Contact> Delete(Guid key)
        {
            try
            {
                var cd =_contactRepository.Find(key as object);
                if (cd.Exception != null) throw cd.Exception;
                var c = _contactRepository.Remove(cd.Result);
                if (c.Exception != null) throw c.Exception;
                return c.Result == null ? (ActionResult<Contact>) BadRequest() : Ok(c.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
   
    }
}
