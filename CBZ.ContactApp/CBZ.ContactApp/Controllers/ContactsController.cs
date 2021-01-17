using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using CBZ.ContactApp.Data;
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
        private readonly ContactDbContext _dbContext;
        private readonly ILogger<ContactsController> _logger;
        private readonly IRepository<Contact> _contactRepository;

        public ContactsController(ContactDbContext context, ILogger<ContactsController> logger,IRepository<Contact> contactRepository)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _contactRepository = contactRepository;
        }
        
        public ActionResult Get()
        {
            try
            {
                var contacts=_contactRepository.Get();
                if (contacts == null)
                {
                    return NoContent();
                }
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Contacts Error");
                return NotFound();
            }
        }
        
        public ActionResult Get(Guid key)
        {
            try
            {
                var contact = _contactRepository.Find(key as object);
                if (contact == null)
                {
                    return NoContent();
                }
                return Ok(contact.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Contacts Error");
                return NotFound();
            }
        }
        
        [HttpGet]
        public ActionResult ByNameSurname(string name,string surname)
        {
            try
            {
                var contacts = _contactRepository.Where(i=>i.Name==name && i.Surname==surname);
                return Ok(contacts);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Contact.ByNameSurname");
                return NoContent();
            }
        }

        public ActionResult Post([FromBody]Contact contact)
        {
            try
            {
                var con=_contactRepository.Add(contact);
                return Ok(con.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Contact creation problem");
                return BadRequest();
            }
          
        }
        
        public ActionResult Put([FromBody]Contact contact)
        {
            try
            {
                var con = _contactRepository.Update(contact);
                return Ok(con.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
                return BadRequest();
            }
        }
        
        public ActionResult Delete([FromBody]Guid key)
        {
            try
            {
                var contactDeleted =_contactRepository.Find(key as object);
                var con=_contactRepository.Remove(contactDeleted.Result);
                return Ok(con.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
                return BadRequest();
            }
        }
   
    }
}
