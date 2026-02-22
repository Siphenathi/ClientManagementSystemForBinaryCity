using ClientManagementSystem.Data.Entities;
using ClientManagementSystem.Model;

namespace ClientManagementSystem.Service;

public interface IClientService
{
	Task<IEnumerable<DisplayClients>> GetAllClientsAsync(bool includeDeletedRecords);
	Task<string> CreateClientAsync(CreateClientRequest createClientRequest);
	Task<IEnumerable<ClientContact>> GetAllClientContactsAsync(int contactId);
	Task<ManageClientContact> GetClientContact(string clientCode);
}