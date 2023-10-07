using Contracts;
using Entities;
using Entities.Models;
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
        public IEnumerable<Client> GetClients(Guid companyId, bool trackChanges) =>
        FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
         .OrderBy(e => e.Name);
        public Client GetClient(Guid companyId, Guid id, bool trackChanges) =>
        FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id),
        trackChanges).SingleOrDefault();
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
