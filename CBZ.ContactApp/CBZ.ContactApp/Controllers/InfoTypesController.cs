﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using CBZ.ContactApp.Data;
using CBZ.ContactApp.Data.Model;
using CBZ.ContactApp.Data.Repository;
using Microsoft.AspNetCore.Http;
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

        public ActionResult<IQueryable<InfoType>> Get()
        {
            try
            {
                var infoTypes=_infoTypeRepository.Get();
                return infoTypes == null ? (ActionResult<IQueryable<InfoType>>)NoContent() : Ok(infoTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get InfoTypes Error");
            }
            return NotFound();
        }
        
        public ActionResult<InfoType> Get(int key)
        {
            try
            {
                var it = _infoTypeRepository.Find(key as object);
                if (it.Exception!=null) throw it.Exception;
                return it.Result == null ? (ActionResult<InfoType>)NoContent() : Ok(it.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception:ex,message:"Get InfoTypes by Keys Error");
            }
            return NotFound();
        }
        
        public ActionResult<InfoType> Post([FromBody]InfoType infoTypes)
        {
            try
            {
                var it = _infoTypeRepository.Add(infoTypes);
                if (it.Exception!=null) throw it.Exception;
                return it.Result == null ? (ActionResult<InfoType>)NoContent() : Ok(it.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Info creation problem");
            }
            return BadRequest();
        }
        
        public ActionResult<InfoType> Put([FromBody]InfoType infoTypes)
        {
            try
            {
                var it = _infoTypeRepository.Update(infoTypes);
                if (it.Exception!=null) throw it.Exception;
                return it.Result == null ? (ActionResult<InfoType>)BadRequest() : Ok(it.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
        
        public ActionResult<InfoType> Delete([FromBody] int key)
        {
            try
            {
                var itd =_infoTypeRepository.Find(key as object);
                if (itd.Exception != null) throw itd.Exception;
                var it = _infoTypeRepository.Remove(itd.Result);
                if (it.Exception != null) throw it.Exception;
                return it.Result == null ? (ActionResult<InfoType>)BadRequest() : Ok(it.Result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception,"Update problem");
            }
            return NotFound();
        }
   
    }
}
