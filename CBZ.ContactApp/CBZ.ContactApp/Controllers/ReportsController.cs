using System;
using System.Linq;
using CBZ.ContactApp.Data.Model;
using CBZ.ContactApp.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter.Value;
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
    public class ReportsController:ODataController
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IRepository<Report> _reportRepository;

        public ReportsController(ILogger<ReportsController> logger, IRepository<Report> reportRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _reportRepository = reportRepository;
        }

        public ActionResult<IQueryable<Report>> Get()
        {
            try
            {
                var reports=_reportRepository.Get();
                return !reports.Any() ? (ActionResult<IQueryable<Report>>)NoContent() : Ok(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Reports Error");
            }
            return NotFound();
        }
        
        public ActionResult<Report> Get(int key)
        {
            try
            {
                var rs = _reportRepository.Find(key as object);
                if (rs.Exception != null) throw rs.Exception;
                return rs.Result == null ? (ActionResult)NoContent() : Ok(rs.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Reports by Keys Error");
            }
            return NotFound();
        }
        
        public ActionResult<Report> Post([FromBody] Report Reports)
        {
            try
            {
                var rs =_reportRepository.Add(Reports);
                if (rs.Exception != null) throw rs.Exception;
                return rs.Result == null ? (ActionResult<Report>)BadRequest() : Ok(rs.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public ActionResult<Report> Put(int key,[FromBody]Delta<Report> reports)
        {
            try
            {
                var rdb = _reportRepository.Find(key as object).Result;
                reports.Put(rdb);
                var rs = _reportRepository.Update(rdb);
                if (rs.Exception != null) throw rs.Exception;
                return rs.Result == null ? (ActionResult<Report>)BadRequest() : Ok(rs.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return BadRequest();
        }
        
        public ActionResult<Report> Patch(int key,[FromBody]Delta<Report> reports)
        {
            try
            {
                var rdb = _reportRepository.Find(key as object).Result;
                reports.Patch(rdb);
                var rs = _reportRepository.Update(rdb);
                if (rs.Exception != null) throw rs.Exception;
                return rs.Result == null ? (ActionResult<Report>)BadRequest() : Ok(rs.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return BadRequest();
        }
        
        public ActionResult<Report> Delete(int key)
        {
            try
            {
                var rd = _reportRepository.Find(key as object);
                if (rd.Exception != null) throw rd.Exception;
                var r = _reportRepository.Remove(rd.Result);
                if (r.Exception != null) throw r.Exception;
                return r.Result == null ? (ActionResult<Report>)BadRequest() : Ok(r.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
    }
}
