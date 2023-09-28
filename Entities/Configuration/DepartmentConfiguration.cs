using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData
            (
            new Department
            {
                Id = new Guid("83abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Accounting",
                CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Department
            {
                Id = new Guid("87abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "IT department",
                CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            }
        );
        }
    }
}
