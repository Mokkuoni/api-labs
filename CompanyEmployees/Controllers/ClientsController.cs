using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies/{companyId}/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ClientsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("{id}", Name = "GetClientForCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var clientDb = _repository.Client.GetClient(companyId, id,
           trackChanges:
            false);
            if (clientDb == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            var client = _mapper.Map<ClientDto>(clientDb);
            return Ok(client);
        }
        [HttpPost]
        public IActionResult CreateClientForCompany(Guid companyId, [FromBody] ClientForCreationDto client)
        {
            if (client == null)
            {
                _logger.LogError("ClientForCreationDto object sent from client is null.");
            return BadRequest("ClientForCreationDto object is null");
            }
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var clientEntity = _mapper.Map<Client>(client);
            _repository.Client.CreateClientForCompany(companyId, clientEntity);
            _repository.Save();
            var clientToReturn = _mapper.Map<ClientDto>(clientEntity);
            return CreatedAtRoute("GetClientForCompany", new
            {
                companyId,
                id = clientToReturn.Id
            }, clientToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteClientForCompany(Guid companyId, Guid id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var clientForCompany = _repository.Client.GetClient(companyId, id, trackChanges: false);
            if (clientForCompany == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _repository.Client.DeleteClient(clientForCompany);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateClientForCompany(Guid companyId, Guid id, [FromBody] ClientForUpdateDto client)
        {
            if (client == null)
            {
                _logger.LogError("ClientForUpdateDto object sent from client is null.");
            return BadRequest("ClientForUpdateDto object is null");
            }
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var clientEntity = _repository.Client.GetClient(companyId, id, trackChanges:true);
            if (clientEntity == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _mapper.Map(client, clientEntity);
            _repository.Save();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateClientForCompany(Guid companyId, Guid id, [FromBody] JsonPatchDocument<ClientForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var clientEntity = _repository.Client.GetClient(companyId, id, trackChanges: true);
            if (clientEntity == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var clientToPatch = _mapper.Map<ClientForUpdateDto>(clientEntity);
            patchDoc.ApplyTo(clientToPatch);
            _mapper.Map(clientToPatch, clientEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
