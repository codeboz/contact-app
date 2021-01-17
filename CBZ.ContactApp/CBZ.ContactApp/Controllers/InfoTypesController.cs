using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Model;
using CBZ.ContactApp.Data.Repository;
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
        private readonly IRepository<InfoType> _infoTypeRepository;

        public InfoTypesController(ContactDbContext context, ILogger<InfoTypesController> logger, IRepository<InfoType> repository)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _infoTypeRepository = repository;
        }

        public ActionResult Get()
        {
            try
            {
                var infoTypes=_infoTypeRepository.Get();
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
        
        public ActionResult Get(int key)
        {
            try
            {
                var it = _infoTypeRepository.Find(key as object);
                if (it == null)
                {
                    return NoContent();
                }
                return Ok(it);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get InfoTypes by Keys Error");
                return NotFound();
            }
        }
        
        public ActionResult Post([FromBody]InfoType infoTypes)
        {
            try
            {
                var it = _infoTypeRepository.Add(infoTypes);
                Ok(it.Result);
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
                var it = _dbContext.InfoTypes.Update(infoTypes);
                await _dbContext.SaveChangesAsync();
                return Ok(it);
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
                var it = _dbContext.InfoTypes.Update(infoTypes.GetInstance());
                await _dbContext.SaveChangesAsync();
                return Ok(it);
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
                var infoTypesDeleted =await _dbContext.InfoTypes.FirstOrDefaultAsync(infoType=>infoType.Id==key);
                var it = _dbContext.InfoTypes.Remove(infoTypesDeleted);
                await _dbContext.SaveChangesAsync();
                return Ok(it);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
   
    }
}
