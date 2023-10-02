using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients(Guid companyId, bool trackChanges);
        Client GetClient(Guid companyId, Guid id, bool trackChanges);
    }
}
