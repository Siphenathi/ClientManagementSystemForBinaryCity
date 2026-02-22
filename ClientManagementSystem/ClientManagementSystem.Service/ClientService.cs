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

		async Task<IEnumerable<DisplayClients>> IClientService.GetAllClientsAsync(bool includeDeletedRecords)
		{
			var listOfClients = await CreateRepository<Client>(connectionString).GetAllAsync(CreateParameter("Deleted", includeDeletedRecords));
			if (listOfClients == null || !listOfClients.Any()) return new List<DisplayClients>();

			var displayClient = new List<DisplayClients>();
			foreach (var client in listOfClients)
			{
				displayClient.Add(new DisplayClients
				{
					ClientCode = client.ClientCode,
					Name = client.Name,
					DateOfRecord = client.DateOfRecord,
					NumberOfContacts = await GetNumberOfClientContacts(includeDeletedRecords, client.ClientCode)
				});
			}
			return displayClient.OrderBy(x => x.Name);
		}

		async Task<IEnumerable<ClientContact>> IClientService.GetAllClientContactsAsync(int contactId)
		{
			var clientContacts = await CreateRepository<ClientContact>(connectionString).GetAllAsync(new Dictionary<string, object>
			{
				{"ContactId", contactId},
				{"Deleted", 0}
			});
			return clientContacts;
		}

		async Task<Client> IClientService.GetClientAsync(string clientCode)
		{
			if (string.IsNullOrWhiteSpace(clientCode)) return new Client();
			var client = await CreateRepository<Client>(connectionString)
				.GetAsync(new Dictionary<string, object>
				{
					{ClientPrimaryKeyColumnName, clientCode},
					{"Deleted", 0}

				});
			return client;
		}

		async Task<string> IClientService.CreateClientAsync(CreateClientRequest createClientRequest)
		{
			var clientCode = UniqueCodeGenerator.GenerateUniqueAlphaNumericHandler(createClientRequest.Name);
			var client = await CreateRepository<Client>(connectionString).GetAsync(new Dictionary<string, object>
			{
				{ ClientPrimaryKeyColumnName, clientCode },
				{ "Name", createClientRequest.Name},
				{ "Deleted", 0}
			});

			if(client != null) return "Client is already registered.";

			var numberOfRowsAffected = await CreateRepository<Client>(connectionString).InsertAsync(new Client
			{
				ClientCode = clientCode,
				Name = createClientRequest.Name,
				DateOfRecord = DateTime.Now,
				DateModified = DateTime.Now
			});

			return numberOfRowsAffected > 0 ? "Client Registered successfully" : "Something went wrong but don't worry our technical team will look at it";
		}

		async Task<string> IClientService.CreateClientContactsAsync(LinkContactsToClient linkContactsToClient)
		{
			var numberOfSavedItems = 0;
			var listOfExistingContactsClient = await CreateRepository<ClientContact>(connectionString)
				.GetAllAsync(CreateParameter(ClientPrimaryKeyColumnName, linkContactsToClient.ClientCode));

			foreach (var clientContact in listOfExistingContactsClient)
			{
				_ = await CreateRepository<ClientContact>(connectionString)
					.DeleteAsync(CreateParameter(ClientPrimaryKeyColumnName, clientContact.ClientCode));
			}

			foreach (var contact in linkContactsToClient.Contacts.Where(x => x.Linked))
			{
				var numberOfRowsAffected = await CreateRepository<ClientContact>(connectionString).InsertAsync(new ClientContact
				{
					ClientCode = linkContactsToClient.ClientCode,
					ContactId = contact.ContactId,
					Deleted = false,
					DateOfRecord = DateTime.Now,
					DateModified = DateTime.Now
				});
				if (numberOfRowsAffected > 0)
					numberOfSavedItems++;
			}

			return numberOfSavedItems == linkContactsToClient.Contacts.Count(x => x.Linked) ? 
				"Client contact(s) are saved successfully" : 
				"Something went wrong, clients contact(s) are not saved";
		}

		async Task<string> IClientService.DeleteClientAsync(string clientCode)
		{
			_ = await CreateRepository<ClientContact>(connectionString)
				.DeleteAsync(CreateParameter(ClientPrimaryKeyColumnName, clientCode));
			var numberOfDeletedClients = await CreateRepository<Client>(connectionString)
				.DeleteAsync(CreateParameter(ClientPrimaryKeyColumnName, clientCode));
			return numberOfDeletedClients > 0 ? "Client deleted successfully" : "Something went wrong but don't worry our technical team will look at it";
		}

		async Task<LinkContactsToClient> IClientService.GetClientContacts(string clientCode)
		{
			var client = await CreateRepository<Client>(connectionString).GetAsync(CreateParameter("ClientCode", clientCode));
			if (client == null) return new LinkContactsToClient();
			
			var allContacts = await CreateRepository<Contact>(connectionString).GetAllAsync();
			var linkContactsToClient = new LinkContactsToClient
			{
				ClientCode = clientCode,
				Name = client.Name,
				DateOfRecord = client.DateOfRecord,
				Contacts = []
			};

			foreach (var contact in allContacts)
			{
				var clientContacts = await CreateRepository<ClientContact>(connectionString).GetAsync(new Dictionary<string, object>
				{
					{"ClientCode", clientCode},
					{"ContactId", contact.ContactId}
				});

				linkContactsToClient.Contacts.Add(new ListOfContacts
				{
					ContactId = contact.ContactId,
					Name = contact.Name,
					Surname = contact.Surname,
					Email = contact.Email,
					Linked = clientContacts != null
				});
			}

			return linkContactsToClient;
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