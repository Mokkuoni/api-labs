using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ClientDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
    }
}
