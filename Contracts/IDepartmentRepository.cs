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
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges);
        Task<Department> GetDepartmentAsync(Guid companyId, Guid id, bool trackChanges);
        void CreateDepartmentForCompany(Guid companyId, Department department);
        void DeleteDepartment(Department department);
    }
}
