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
    public class InfosController:ODataController
    {
        private readonly ContactDbContext _dbContext;
        private readonly ILogger<InfosController> _logger;

        public InfosController(ContactDbContext context, ILogger<InfosController> logger)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

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
                _logger.LogError(exception:ex,message:"Get Infos Error");
                return NotFound();
            }
        }
        
        public async Task<IActionResult> Get(Guid contactId,int infoTypeId)
        {
            try
            {
                var i = await _dbContext.Infos.SingleOrDefaultAsync(info=>info.ContactId==contactId && info.InfoTypeId==infoTypeId);
                if (i == null)
                {
                    return NoContent();
                }
                return Ok(i);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Infos by Keys Error");
                return NotFound();
            }
        }
        public async Task<IActionResult> Post([FromBody]Info info)
        {
            try
            {
                var i = await _dbContext.Infos.AddAsync(info);
                await _dbContext.SaveChangesAsync();
                Ok(i);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public async Task<IActionResult> Put([FromBody]Info info)
        {
            try
            {
                var i = _dbContext.Infos.Update(info);
                await _dbContext.SaveChangesAsync();
                return Ok(i);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Patch(Guid id,[FromBody] Delta<Info> info)
        {
            try
            {
                var i = _dbContext.Infos.Update(info.GetInstance());
                await _dbContext.SaveChangesAsync();
                return Ok(i);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Delete([FromBody] Guid contactId,[FromBody]int infoTypeId)
        {
            try
            {
                var infoDeleted =await _dbContext.Infos.FirstOrDefaultAsync(info=>info.ContactId == contactId && info.InfoTypeId==infoTypeId);
                var i = _dbContext.Infos.Remove(infoDeleted);
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
