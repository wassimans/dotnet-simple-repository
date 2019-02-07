using System;
using Contracts;
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
    }
}