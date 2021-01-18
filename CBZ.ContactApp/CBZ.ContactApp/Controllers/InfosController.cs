using System;
using System.Linq;
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

        public ActionResult<IQueryable<Info>> Get()
        {
            try
            {
                var infos=_infoRepository.Get();
                return !infos.Any() ? (ActionResult<IQueryable<Info>>)NoContent() : Ok(infos);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Infos Error");
            }
            return NotFound();
        }
        
        public ActionResult<Info> Get(Guid keyContactId,int keyInfoTypeId)
        {
            try
            {
                var i = _infoRepository.Find(keyContactId as object, keyInfoTypeId as object);
                if (i.Exception!=null) throw i.Exception;
                return i.Result == null ? (ActionResult<Info>)NoContent() : Ok(i.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get Infos by Keys Error");
            }
            return NotFound();
        }
        public ActionResult<Info> Post([FromBody]Info info)
        {
            try
            {
                var i = _infoRepository.Add(info);
                if (i.Exception!=null) throw i.Exception;
                return i.Result == null ? (ActionResult<Info>)NoContent() : Ok(i.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public ActionResult<Info> Put([FromBody]Info info)
        {
            try
            {
                var i = _infoRepository.Update(info);
                if (i.Exception != null) throw i.Exception;
                return i.Result == null ? (ActionResult<Info>) BadRequest() : Ok(i.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return BadRequest();
        }

        public ActionResult<Info> Delete([FromBody] Guid contactId,[FromBody]int infoTypeId)
        {
            try
            {
                var id = _infoRepository.Find(contactId as object, infoTypeId as object);
                if (id.Exception != null) throw id.Exception;
                var i = _infoRepository.Remove(id.Result);
                if (i.Exception != null) throw i.Exception;
                return i.Result == null ? (ActionResult<Info>) BadRequest() : Ok(i.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
    }
}
