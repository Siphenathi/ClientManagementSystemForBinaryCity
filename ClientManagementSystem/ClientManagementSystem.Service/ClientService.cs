using ClientManagementSystem.Data.Entities;
using ClientManagementSystem.Model;
using GenericRepo.Dapper.Wrapper; //For more info: https://github.com/Siphenathi/GenericRepo.Dapper.Wrapper
using GenericRepo.Dapper.Wrapper.Domain;
using GenericRepo.Dapper.Wrapper.Interface;

namespace ClientManagementSystem.Service
{
	public class ClientService(string connectionString) : IClientService
	{
		private const string ClientPrimaryKeyColumnName = "ClientCode";

		async Task<IEnumerable<Client>> IClientService.GetAllClientsAsync(bool includeDeletedRecords)
		{
			var listOfClients = await CreateRepository<Client>(connectionString).GetAllAsync(CreateParameter("Deleted", includeDeletedRecords));
			if (listOfClients == null || !listOfClients.Any()) return new List<Client>();
			foreach (var client in listOfClients)
			{
				client.NumberOfContacts = await GetNumberOfClientContacts(includeDeletedRecords, client.ClientCode);
			}
			return listOfClients.OrderBy(x => x.Name);
		}

		async Task<IEnumerable<ClientContact>> IClientService.GetAllClientContactsAsync(int contactId)
		{
			var clientContacts = await CreateRepository<ClientContact>(connectionString).GetAllAsync(CreateParameter("ContactId", contactId));
			return clientContacts;
		}

		async Task<string> IClientService.CreateClientAsync(CreateClientRequest createClientRequest)
		{
			var clientCode = "332"; //Todo: generate the code according to the spec
			var client = await CreateRepository<Client>(connectionString).GetAsync(new Dictionary<string, object>
			{
				{ ClientPrimaryKeyColumnName, clientCode },
				{ "Name", createClientRequest.Name}
			});

			if(client != null) return "Client is already registered.";

			var numberOfRowsAffected = await CreateRepository<Client>(connectionString).InsertAsync(new Client
			{
				ClientCode = clientCode,
				Name = createClientRequest.Name
			});

			return numberOfRowsAffected > 0 ? "Client Registered successfully" : "Something went wrong but don't worry our technical team is working on it";
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

		private async Task<int> GetNumberOfClientContacts(bool includeDeletedRecords, string clientCode)
		{
			var clientContact = await CreateRepository<ClientContact>(connectionString).GetAllAsync(
				new Dictionary<string, object>
				{
					{ "Deleted", includeDeletedRecords },
					{ "ClientCode", clientCode }
				});
			return clientContact.Count();
		}
	}
}