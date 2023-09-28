using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Department
    {
        [Column("DepartmentId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Department name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Client> Clients { get; set; }

    }
}
