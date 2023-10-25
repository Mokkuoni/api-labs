using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CompanyForCreationDto
    {
        public IEnumerable<EmployeeForCreationDto> Employees { get; set; }
        public IEnumerable<DepartmentForCreationDto> Departments { get; set; }
        public IEnumerable<ClientForCreationDto> Clients { get; set; }
    }
}
