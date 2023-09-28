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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData
            (
            new Client
            {
                Id = new Guid("84abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Ivan Ivanov",
                Age = 58,
                PhoneNumber = "89099099999",
                CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Client
            {
                Id = new Guid("85abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Denis Denisov",
                Age = 35,
                PhoneNumber = "84504504545",
                CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            }
        );
        }
    }
}
