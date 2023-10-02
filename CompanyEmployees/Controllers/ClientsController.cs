using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
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
        [HttpGet("{id}")]
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
    }
}
