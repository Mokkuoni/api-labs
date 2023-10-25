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
        Task<IEnumerable<Client>> GetAllClientsAsync(bool trackChanges);
        Task<Client> GetClientAsync(Guid companyId, Guid id, bool trackChanges);
        void CreateClientForCompany(Guid companyId, Client client);
        void DeleteClient(Client client);
    }
}
