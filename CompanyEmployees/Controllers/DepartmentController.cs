using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies/{companyId}/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public DepartmentController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet("{id}", Name = "GetDepartmentForCompany")]
        public IActionResult GetDepartmentForCompany(Guid companyId, Guid id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var departmentDb = _repository.Department.GetDepartment(companyId, id,
           trackChanges:
            false);
            if (departmentDb == null)
            {
                _logger.LogInfo($"Department with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            var department = _mapper.Map<DepartmentDto>(departmentDb);
            return Ok(department);
        }
        [HttpPost]
        public IActionResult CreateDepartmentForCompany(Guid companyId, [FromBody] DepartmentForCreationDto department)
        {
            if (department == null)
            {
                _logger.LogError("DepartmentForCreationDto object sent from client is null.");
            return BadRequest("DepartmentForCreationDto object is null");
            }
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var departmentEntity = _mapper.Map<Department>(department);
            _repository.Department.CreateDepartmentForCompany(companyId, departmentEntity);
            _repository.Save();
            var departmentToReturn = _mapper.Map<DepartmentDto>(departmentEntity);
            return CreatedAtRoute("GetDepartmentForCompany", new
            {
                companyId,
                id = departmentToReturn.Id
            }, departmentToReturn);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartmentForCompany(Guid companyId, Guid id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var departmentForCompany = _repository.Department.GetDepartment(companyId, id, trackChanges: false);
            if (departmentForCompany == null)
            {
                _logger.LogInfo($"Department with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _repository.Department.DeleteDepartment(departmentForCompany);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateDepartmentForCompany(Guid companyId, Guid id, [FromBody] DepartmentForUpdateDto department)
        {
            if (department == null)
            {
                _logger.LogError("DepartmentForUpdateDto object sent from client is null.");
            return BadRequest("DepartmentForUpdateDto object is null");
            }
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var departmentEntity = _repository.Department.GetDepartment(companyId, id, trackChanges: true);
            if (departmentEntity == null)
            {
                _logger.LogInfo($"Department with id: {id} doesn't exist in the database.");
            return NotFound();
            }
            _mapper.Map(department, departmentEntity);
            _repository.Save();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateDepartmentForCompany(Guid companyId, Guid id, [FromBody] JsonPatchDocument<DepartmentForUpdateDto> patchDoc)
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
            var departmentEntity = _repository.Department.GetDepartment(companyId, id, trackChanges: true);
            if (departmentEntity == null)
            {
                _logger.LogInfo($"Department with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var departmentToPatch = _mapper.Map<DepartmentForUpdateDto>(departmentEntity);
            patchDoc.ApplyTo(departmentToPatch);
            _mapper.Map(departmentToPatch, departmentEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
