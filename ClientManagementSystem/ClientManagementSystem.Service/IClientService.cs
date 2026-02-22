using ClientManagementSystem.Data.Entities;
using ClientManagementSystem.Model;

namespace ClientManagementSystem.Service;

public interface IClientService
{
	Task<IEnumerable<DisplayClients>> GetAllClientsAsync(bool includeDeletedRecords);
	Task<Client> GetClientAsync(string clientCode);
	Task<string> CreateClientAsync(CreateClientRequest createClientRequest);
	Task<string> CreateClientContactAsync(LinkContactsToClient linkContactsToClient);
	Task<IEnumerable<ClientContact>> GetAllClientContactsAsync(int contactId);
	Task<LinkContactsToClient> GetClientContacts(string clientCode);
	Task<string> DeleteClientAsync(string clientCode);
}