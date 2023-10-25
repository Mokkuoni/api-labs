using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges)
        => await FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToListAsync();
        public async Task<Department> GetDepartmentAsync(Guid companyId, Guid id, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        .SingleOrDefaultAsync();
        public void CreateDepartmentForCompany(Guid companyId, Department department)
        {
            department.CompanyId = companyId;
            Create(department);
        }
        public void DeleteDepartment(Department department)
        {
            Delete(department);
        }
    }
}
