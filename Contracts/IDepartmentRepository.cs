using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments(Guid companyId, bool trackChanges);
        Department GetDepartment(Guid companyId, Guid id, bool trackChanges);
        void CreateDepartmentForCompany(Guid companyId, Department department);
    }
}
