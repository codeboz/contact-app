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
    public class ReportStatesController:ODataController
    {
        private readonly ContactDbContext _dbContext;
        private readonly ILogger<ReportStatesController> _logger;

        public ReportStatesController(ContactDbContext context, ILogger<ReportStatesController> logger)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ActionResult Get()
        {
            try
            {
                var reportRequests=_dbContext.ReportStates.AsQueryable();
                if (reportRequests == null)
                {
                    return NoContent();
                }
                return Ok(reportRequests);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get ReportStates Error");
                return NotFound();
            }
        }
        
        public async Task<IActionResult> Get(int key)
        {
            try
            {
                var rs = await _dbContext.ReportStates.SingleOrDefaultAsync(reportState => reportState.Id==key);
                if (rs == null)
                {
                    return NoContent();
                }
                return Ok(rs);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get ReportStates by Keys Error");
                return NotFound();
            }
        }
        public async Task<IActionResult> Post([FromBody] ReportState reportStates)
        {
            try
            {
                var rs = await _dbContext.ReportStates.AddAsync(reportStates);
                await _dbContext.SaveChangesAsync();
                Ok(rs);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public async Task<IActionResult> Put([FromBody] ReportState reportRequests)
        {
            try
            {
                var rs = _dbContext.ReportStates.Update(reportRequests);
                await _dbContext.SaveChangesAsync();
                return Ok(rs);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Patch(Guid id,[FromBody] Delta<ReportState> reportStates)
        {
            try
            {
                var rs = _dbContext.ReportStates.Update(reportStates.GetInstance());
                await _dbContext.SaveChangesAsync();
                return Ok(rs);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Report request update problem");
            }
            return NotFound();
        }
        
        public async Task<IActionResult> Delete([FromBody] int key)
        {
            try
            {
                var reportRequestsDeleted =await _dbContext.ReportStates.FirstOrDefaultAsync(reportState=>reportState.Id==key);
                var rs = _dbContext.ReportStates.Remove(reportRequestsDeleted);
                await _dbContext.SaveChangesAsync();
                return Ok(rs);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
   
    }
}
