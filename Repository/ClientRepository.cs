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
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Client>> GetAllClientsAsync(bool trackChanges)
        => await FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToListAsync();
        public async Task<Client> GetClientAsync(Guid companyId, Guid id, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        .SingleOrDefaultAsync();
        public void CreateClientForCompany(Guid companyId, Client client)
        {
            client.CompanyId = companyId;
            Create(client);
        }
        public void DeleteClient(Client client)
        {
            Delete(client);
        }
    }
}
