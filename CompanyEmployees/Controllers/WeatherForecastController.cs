using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace CompanyEmployees.Controllers

{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public WeatherForecastController(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var company = new Company { Id = Guid.NewGuid(), Name = "name", Address = "address", Country = "USA" };
            var anotherCompany = new Company { Id = Guid.NewGuid(), Name = "ITCompany", Address = "addres", Country = "Russia" };
            _repository.Company.Create(company);
            _repository.Company.Create(anotherCompany);
            _repository.Save();
            return new string[] { "value1", "value2", "value3", "value4" };
        }
    }

}
