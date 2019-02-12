using System;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        public OwnerController (ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper)
        {
            _logger = loggerManager;
            _repository = repositoryWrapper;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var result = _repository.Owner.FindAll();
                _logger.LogInfo($"Returned all owners from database.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetALLOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnerById(Guid id)
        {
            try
            {
                var result = _repository.Owner.GetOwnerById(id);

                if (result.OwnerId.Equals(Guid.Empty))
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in the database");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/account")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            try
            {
                var result  = _repository.Owner.GetOwnerWithDetails(id);

                if (result.OwnerId.Equals(Guid.Empty))
                {
                    _logger.LogInfo($"Owner with id: {id}, hasn't been found in the database");
                    return NotFound();
                }
                else {
                    _logger.LogInfo($"Returned owner details with id: {id}");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateOwner([FromBody]Owner owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Owner.CreateOwner(owner);
                return CreatedAtRoute("OwnerById", new { id = owner.OwnerId}, owner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Interal server error");
            }
        }
    }
}