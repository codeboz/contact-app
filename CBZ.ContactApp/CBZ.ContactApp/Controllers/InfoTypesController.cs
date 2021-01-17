using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Formatter.Value;
using Microsoft.Extensions.Logging;

namespace CBZ.ContactApp.Controllers
{
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class InfoTypesController:ODataController
    {
        private readonly ContactDbContext _dbContext;
        private readonly ILogger<InfoTypesController> _logger;

        public InfoTypesController(ContactDbContext context, ILogger<InfoTypesController> logger)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ActionResult Get()
        {
            try
            {
                var infoTypes=_dbContext.InfoTypes.AsQueryable();
                if (infoTypes == null)
                {
                    return NoContent();
                }
                return Ok(infoTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get InfoTypes Error");
                return NotFound();
            }
        }
        
        public async Task<IActionResult> Get(int key)
        {
            try
            {
                var i = await _dbContext.InfoTypes.SingleOrDefaultAsync(i=>i.Id==key);
                if (i == null)
                {
                    return NoContent();
                }
                return Ok(i);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get InfoTypes by Keys Error");
                return NotFound();
            }
        }
        public async Task<IActionResult> Post([FromBody]InfoType infoTypes)
        {
            try
            {
                var i = await _dbContext.InfoTypes.AddAsync(infoTypes);
                await _dbContext.SaveChangesAsync();
                Ok(i);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public async Task<IActionResult> Put([FromBody]InfoType infoTypes)
        {
            try
            {
                var i = _dbContext.InfoTypes.Update(infoTypes);
                await _dbContext.SaveChangesAsync();
                return Ok(i);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Patch(Guid id,[FromBody] Delta<InfoType> infoTypes)
        {
            try
            {
                var i = _dbContext.InfoTypes.Update(infoTypes.GetInstance());
                await _dbContext.SaveChangesAsync();
                return Ok(i);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Delete([FromBody] int key)
        {
            try
            {
                var infoTypesDeleted =await _dbContext.InfoTypes.FirstOrDefaultAsync(i=>i.Id==key);
                var i = _dbContext.InfoTypes.Remove(infoTypesDeleted);
                await _dbContext.SaveChangesAsync();
                return Ok(i);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
   
    }
}
