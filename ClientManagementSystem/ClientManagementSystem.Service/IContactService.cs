using ClientManagementSystem.Model;

namespace ClientManagementSystem.Service;

public interface IContactService
{
	Task<IEnumerable<DisplayContact>> GetAllContactsAsync(bool includeDeletedRecords = false);
	Task<string> CreateContactAsync(CreateContactRequest createContactRequest);
	Task<string> CreateContactClientsAsync(LinkClientsToContact linkClientsToContact);
	Task<LinkClientsToContact> GetContactClients(int contactId);
}