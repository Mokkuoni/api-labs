using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Client
    {
        [Column("ClientId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Client name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Age is a required field.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "PhoneNumber is a required field.")]
        [MaxLength(11, ErrorMessage = "Maximum length for the PhoneNumber is 11 characters.")]
        public string PhoneNumber { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
