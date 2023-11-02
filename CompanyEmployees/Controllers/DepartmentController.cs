using AutoMapper;
using CompanyEmployees.ActionFilters;
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
        public async Task<IActionResult> GetDepartmentForCompany(Guid companyId, Guid id)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var departmentDb = await _repository.Department.GetDepartmentAsync(companyId, id,
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDepartmentForCompany(Guid companyId, [FromBody] DepartmentForCreationDto department)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var departmentEntity = _mapper.Map<Department>(department);
            _repository.Department.CreateDepartmentForCompany(companyId, departmentEntity);
            await _repository.SaveAsync();
            var departmentToReturn = _mapper.Map<DepartmentDto>(departmentEntity);
            return CreatedAtRoute("GetDepartmentForCompany", new
            {
                companyId,
                id = departmentToReturn.Id
            }, departmentToReturn);
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateDepartmentForCompanyExistsAttribute))]
        public async Task<IActionResult> DeleteDepartmentForCompany(Guid companyId, Guid id)
        {
            var departmentForCompany = HttpContext.Items["department"] as Department;
            _repository.Department.DeleteDepartment(departmentForCompany);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateDepartmentForCompanyExistsAttribute))]
        public async Task<IActionResult> UpdateDepartmentForCompany(Guid companyId, Guid id, [FromBody] DepartmentForUpdateDto department)
        {
            var departmentEntity = HttpContext.Items["department"] as Department;
            _mapper.Map(department, departmentEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateDepartmentForCompanyExistsAttribute))]
        public IActionResult PartiallyUpdateDepartmentForCompany(Guid companyId, Guid id, [FromBody] JsonPatchDocument<DepartmentForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var departmentEntity = HttpContext.Items["department"] as Department;
            var departmentToPatch = _mapper.Map<DepartmentForUpdateDto>(departmentEntity);
            patchDoc.ApplyTo(departmentToPatch, ModelState);
            TryValidateModel(departmentToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(departmentToPatch, departmentEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
