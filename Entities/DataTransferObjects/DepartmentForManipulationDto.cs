using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract class DepartmentForManipulationDto
    {
        [Required(ErrorMessage = "Department name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 3 characters.")]
        public string Name { get; set; }
    }
}
