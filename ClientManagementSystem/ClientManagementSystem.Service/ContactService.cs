using ClientManagementSystem.Data.Entities;
using GenericRepo.Dapper.Wrapper.Domain;
using GenericRepo.Dapper.Wrapper.Interface;
using GenericRepo.Dapper.Wrapper;

namespace ClientManagementSystem.Service
{
    public class ContactService(string connectionString) : IContactService
	{
		async Task<IEnumerable<Contact>> IContactService.GetAllContactsAsync(bool includeDeletedRecords)
		{
			var listOfContacts = await CreateRepository<Contact>(connectionString).GetAllAsync(CreateParameter("Deleted", includeDeletedRecords));
			foreach (var contact in listOfContacts)
			{
				var clients = await CreateRepository<ClientContact>(connectionString).GetAllAsync(CreateParameter("ContactId", contact.ContactId));
				//contact.NumberOfClients = clients.Count();
			}

			return listOfContacts;
		}

		//async Task<IEnumerable<Client>> IClientService.GetAllClientsAsync(bool includeDeletedRecords)
		//{
		//	var listOfClients = await CreateRepository<Client>(connectionString).GetAllAsync(CreateParameter("Deleted", includeDeletedRecords));
		//	foreach (var client in listOfClients)
		//	{
		//		client.Contacts = CreateRepository<Contact>(connectionString).
		//	}
		//	return listOfClients.OrderBy(x => x.Name);
		//}

		//async Task<string> IContactService.CreateContactAsync(CreateContacRequest createClientRequest)
		//{
		//	throw new NotImplementedException();
		//}

		async Task<IEnumerable<ClientContact>> IContactService.GetAllClientContactsAsync(int contactId)
		{
			throw new NotImplementedException();
		}

		private static IRepository<T> CreateRepository<T>(string connectionString) where T : class
		{
			return string.IsNullOrWhiteSpace(connectionString) ?
				throw new NullReferenceException("ConnectionString cannot be null") :
				new Repository<T>($"{typeof(T).Name}s", connectionString, DatabaseProvider.MySql);
		}

		private static Dictionary<string, object> CreateParameter(string parameterName, object value)
		{
			return new Dictionary<string, object>
			{
				{ parameterName, value }
			};
		}
	}

    public interface IContactService
    {
	    Task<IEnumerable<Contact>> GetAllContactsAsync(bool includeDeletedRecords);
	    //Task<string> CreateContactAsync(CreateContacRequest createClientRequest);
	    Task<IEnumerable<ClientContact>> GetAllClientContactsAsync(int contactId);
	}
}