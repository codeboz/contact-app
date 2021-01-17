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
    public class ReportStatesController:ODataController
    {
        private readonly ContactDbContext _dbContext;
        private readonly ILogger<ReportStatesController> _logger;
        private readonly IRepository<ReportState> _reportStateRepository;

        public ReportStatesController(ContactDbContext context, ILogger<ReportStatesController> logger, IRepository<ReportState> reportStateRepository)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _reportStateRepository = reportStateRepository;
        }

        public ActionResult Get()
        {
            try
            {
                var reportRequests=_reportStateRepository.Get();
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
        
        public ActionResult Get(int key)
        {
            try
            {
                var rs = _reportStateRepository.Find(key as object);
                if (rs.Exception != null) throw rs.Exception;
                if (rs.Result == null)
                {
                    return NoContent();
                }
                return Ok(rs.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get ReportStates by Keys Error");
                return NotFound();
            }
        }
        public ActionResult Post([FromBody] ReportState reportStates)
        {
            try
            {
                var rs =_reportStateRepository.Add(reportStates);
                if (rs.Exception != null) throw rs.Exception;
                Ok(rs.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public ActionResult Put([FromBody] ReportState reportRequests)
        {
            try
            {
                var rs = _reportStateRepository.Update(reportRequests);
                if (rs.Exception != null) throw rs.Exception;
                return Ok(rs.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
        
        public ActionResult Delete([FromBody] int key)
        {
            try
            {
                var reportRequestsDeleted = _reportStateRepository.Find(key as object);
                if (reportRequestsDeleted.Exception != null) throw reportRequestsDeleted.Exception;
                var rs = _reportStateRepository.Remove(reportRequestsDeleted.Result);
                return Ok(rs.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
    }
}
