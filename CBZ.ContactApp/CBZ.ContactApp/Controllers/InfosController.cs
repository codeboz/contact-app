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
    public class InfosController:ODataController
    {
        private readonly ContactDbContext _dbContext;
        private readonly ILogger<InfosController> _logger;
        private readonly IRepository<Info> _infoRepository;

        public InfosController(ContactDbContext context, ILogger<InfosController> logger, IRepository<Info> repository)
        {
            _dbContext = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _infoRepository = repository;
        }

        public ActionResult Get()
        {
            try
            {
                var infos=_infoRepository.Get();
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
        
        public ActionResult Get(Guid keyContactId,int keyInfoTypeId)
        {
            try
            {
                var i = _infoRepository.Find(keyContactId, keyInfoTypeId as object); 
                if (i == null)
                {
                    return NoContent();
                }
                return Ok(i.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Infos by Keys Error");
                return NotFound();
            }
        }
        public ActionResult Post([FromBody]Info info)
        {
            try
            {
                var i = _infoRepository.Add(info);
                Ok(i.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public ActionResult Put([FromBody]Info info)
        {
            try
            {
                var i = _infoRepository.Update(info);
                return Ok(i.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }

        public ActionResult Delete([FromBody] Guid contactId,[FromBody]int infoTypeId)
        {
            try
            {
                var infoDeleted = _infoRepository.Find(contactId, infoTypeId as object);
                var i = _infoRepository.Remove(infoDeleted.Result);
                return Ok(i.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
    }
}
