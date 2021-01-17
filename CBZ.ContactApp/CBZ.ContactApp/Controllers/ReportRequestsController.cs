using System;
using System.Threading.Tasks;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter.Value;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CBZ.ContactApp.Controllers
{
    [EnableQuery]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ReportRequestsController:ODataController
    {
        private readonly ContactDbContext _dbContext;
        private readonly ILogger<ReportRequestsController> _logger;

        public ReportRequestsController(ContactDbContext context, ILogger<ReportRequestsController> logger)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ActionResult Get()
        {
            try
            {
                var reportRequests=_dbContext.ReportRequests.AsQueryable();
                if (reportRequests == null)
                {
                    return NoContent();
                }
                return Ok(reportRequests);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get ReportRequests Error");
                return NotFound();
            }
        }
        
        public async Task<IActionResult> Get(Guid key)
        {
            try
            {
                var rr = await _dbContext.ReportRequests.SingleOrDefaultAsync(reportRequest=>reportRequest.Id==key);
                if (rr == null)
                {
                    return NoContent();
                }
                return Ok(rr);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get ReportRequests by Keys Error");
                return NotFound();
            }
        }
        public async Task<IActionResult> Post([FromBody] ReportRequest reportRequest)
        {
            try
            {
                var rr = await _dbContext.ReportRequests.AddAsync(reportRequest);
                await _dbContext.SaveChangesAsync();
                Ok(rr);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public async Task<IActionResult> Put([FromBody]ReportRequest reportRequests)
        {
            try
            {
                var rr = _dbContext.ReportRequests.Update(reportRequests);
                await _dbContext.SaveChangesAsync();
                return Ok(rr);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Patch(Guid id,[FromBody] Delta<ReportRequest> reportRequest)
        {
            try
            {
                var rr = _dbContext.ReportRequests.Update(reportRequest.GetInstance());
                await _dbContext.SaveChangesAsync();
                return Ok(rr);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Report request update problem");
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Delete([FromBody] Guid key)
        {
            try
            {
                var reportRequestsDeleted =await _dbContext.ReportRequests.FirstOrDefaultAsync(reportRequest=>reportRequest.Id==key);
                var rr = _dbContext.ReportRequests.Remove(reportRequestsDeleted);
                await _dbContext.SaveChangesAsync();
                return Ok(rr);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
   
    }
}
