using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Formatter.Value;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CBZ.ContactApp.Controllers
{
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class InfosController:ODataController
    {
        private readonly ContactDbContext _dbContext;
        private readonly ILogger<ContactsController> _logger;

        public InfosController(ContactDbContext context, ILogger<ContactsController> logger)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var infos=_dbContext.Infos.AsQueryable();
                if (infos == null)
                {
                    return NoContent();
                }
                return Ok(infos);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Contacts Error");
                return NotFound();
            }
        }
        
        public async Task<IActionResult> Get(Guid key)
        {
            try
            {
                var contact = await _dbContext.Contacts.SingleOrDefaultAsync(c => c.Id == key);
                if (contact == null)
                {
                    return NoContent();
                }
                return Ok(contact);
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
                var contacts = _dbContext.Contacts.Where(c => c.Name == name && c.Surname == surname);
                return Ok(contacts);
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Contact.ByNameSurname");
                return NoContent();
            }
        }

        public async Task<IActionResult> Post([FromBody]Contact contact)
        {
            var con = await _dbContext.Contacts.AddAsync(contact);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Create problem");
                return BadRequest();
            }
            return Ok(con);
        }
        
        public async Task<IActionResult> Put([FromBody]Contact contact)
        {
            try
            {
                var con = _dbContext.Contacts.Update(contact);
                await _dbContext.SaveChangesAsync();
                return Ok(con);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
                return BadRequest();
            }
        }
        
        public async Task<IActionResult> Patch(Guid id,[FromBody] Delta<Contact> contact)
        {
            try
            {
                var con = _dbContext.Contacts.Update(contact.GetInstance());
                await _dbContext.SaveChangesAsync();
                return Ok(con);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
                return BadRequest();
            }
        }
        
        public async Task<IActionResult> Delete([FromBody]Guid id)
        {
            try
            {
                var contactDeleted =await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == id);
                var con = _dbContext.Contacts.Remove(contactDeleted);
                await _dbContext.SaveChangesAsync();
                return Ok(con);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
                return BadRequest();
            }
        }
   
    }
}
