using System;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Model;
using CBZ.ContactApp.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
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
        private readonly IRepository<ReportRequest> _reportRequestRepository;


        public ReportRequestsController(ContactDbContext context, ILogger<ReportRequestsController> logger, IRepository<ReportRequest> repository)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _reportRequestRepository = repository;
        }

        public ActionResult Get()
        {
            try
            {
                var reportRequests=_reportRequestRepository.Get();
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
        
        public IActionResult Get(Guid key)
        {
            try
            {
                var rr = _reportRequestRepository.Find(key as object);
                if (rr == null)
                {
                    return NoContent();
                }
                return Ok(rr.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get ReportRequests by Keys Error");
                return NotFound();
            }
        }
        public ActionResult Post([FromBody] ReportRequest reportRequest)
        {
            try
            {
                var rr = _reportRequestRepository.Add(reportRequest);
                if (rr.Exception != null)
                {
                    throw rr.Exception;
                }
                Ok(rr.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public ActionResult Put([FromBody]ReportRequest reportRequests)
        {
            try
            {
                var rr = _reportRequestRepository.Update(reportRequests);
                if (rr.Exception != null)
                {
                    throw rr.Exception;
                }
                Ok(rr.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }

        public ActionResult Delete([FromBody] Guid key)
        {
            try
            {
                var reportRequestsDeleted = _reportRequestRepository.Find(key as object);
                if (reportRequestsDeleted.Exception != null)
                {
                    throw reportRequestsDeleted.Exception;
                }
                var rr = _reportRequestRepository.Remove(reportRequestsDeleted.Result);
                if (rr.Exception != null)
                {
                    throw rr.Exception;
                }
                return Ok(rr.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
   
    }
}
