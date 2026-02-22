using ClientManagementSystem.Data.Entities;
using ClientManagementSystem.Model;
using GenericRepo.Dapper.Wrapper.Domain;
using GenericRepo.Dapper.Wrapper.Interface;
using GenericRepo.Dapper.Wrapper;
using Client = ClientManagementSystem.Data.Entities.Client;

namespace ClientManagementSystem.Service
{
    public class ContactService(string connectionString) : IContactService
	{
		async Task<IEnumerable<DisplayContact>> IContactService.GetAllContactsAsync(bool includeDeletedRecords)
		{
			var listOfContacts = await CreateRepository<Contact>(connectionString).GetAllAsync(CreateParameter("Deleted", includeDeletedRecords));
			if (listOfContacts == null || !listOfContacts.Any()) return new List<DisplayContact>();

			var displayContact = new List<DisplayContact>();
			foreach (var contact in listOfContacts)
			{
				displayContact.Add(new DisplayContact
				{
					ContactId = contact.ContactId,
					Name = contact.Name,
					Surname = contact.Surname,
					Email = contact.Email,
					NumberOfContacts = await GetNumberOfClientsPerContact(includeDeletedRecords, contact.ContactId)
				});
			}
			return displayContact.OrderBy(m => m.Name).ThenBy(m => m.Surname).ToList() ;
		}

		async Task<string> IContactService.CreateContactAsync(CreateContactRequest createContactRequest)
		{
			var contact = await CreateRepository<Contact>(connectionString).GetAsync(new Dictionary<string, object>
			{
				{ "Email", createContactRequest.Email},
				{ "Deleted", 0}
			});

			if (contact != null) return "Contact already Exist.";
			var numberOfRowsAffected = await CreateRepository<Contact>(connectionString).InsertAsync(new Contact
			{
				Name = createContactRequest.Name,
				Surname = createContactRequest.Surname,
				Email = createContactRequest.Email,
				DateOfRecord = DateTime.Now,
				DateModified = DateTime.Now
			});

			return numberOfRowsAffected > 0 ? "Contact Registered successfully" : "Something went wrong but don't worry our technical team will look at it";
		}

		async Task<string> IContactService.CreateContactClientsAsync(LinkClientsToContact linkClientsToContact)
		{
			var numberOfSavedItems = 0;
			var listOfExistingClientsContact = await CreateRepository<ClientContact>(connectionString)
				.GetAllAsync(CreateParameter("ContactId", linkClientsToContact.ContactId));

			foreach (var clientContact in listOfExistingClientsContact)
			{
				_ = await CreateRepository<ClientContact>(connectionString)
					.DeleteAsync(CreateParameter("ContactId", clientContact.ContactId));
			}

			foreach (var client in linkClientsToContact.Clients.Where(x => x.Linked))
			{
				var numberOfRowsAffected = await CreateRepository<ClientContact>(connectionString).InsertAsync(new ClientContact
				{
					ClientCode = client.ClientCode,
					ContactId = linkClientsToContact.ContactId,
					Deleted = false,
					DateOfRecord = DateTime.Now,
					DateModified = DateTime.Now
				});
				if (numberOfRowsAffected > 0)
					numberOfSavedItems++;
			}

			return numberOfSavedItems == linkClientsToContact.Clients.Count(x => x.Linked) ?
				"Clients are linked successfully" :
				"Something went wrong, clients are not linked";
		}

		async Task<LinkClientsToContact> IContactService.GetContactClients(int contactId)
		{
			var contact = await CreateRepository<Contact>(connectionString).GetAsync(CreateParameter("ContactId", contactId));
			if (contact == null) return new LinkClientsToContact();

			var allClients = await CreateRepository<Client>(connectionString).GetAllAsync();
			var linkClientsToContact = new LinkClientsToContact
			{
				ContactId = contactId,
				Name = contact.Name,
				Surname = contact.Surname,
				Email = contact.Email,
				DateOfRecord = contact.DateOfRecord,
				Clients = []
			};

			foreach (var client in allClients)
			{
				var clientContacts = await CreateRepository<ClientContact>(connectionString).GetAsync(new Dictionary<string, object>
				{
					{"ClientCode", client.ClientCode},
					{"ContactId", contact.ContactId}
				});

				linkClientsToContact.Clients.Add(new ListOfClients
				{
					ClientCode = client.ClientCode,
					Name = client.Name,
					Linked = clientContacts != null
				});
			}

			return linkClientsToContact;
		}

		private async Task<int> GetNumberOfClientsPerContact(bool includeDeletedRecords, int clientCode)
		{
			var clientContact = await CreateRepository<ClientContact>(connectionString).GetAllAsync(
				new Dictionary<string, object>
				{
					{ "Deleted", includeDeletedRecords },
					{ "ContactId", clientCode }
				});
			return clientContact.Count();
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
	    Task<IEnumerable<DisplayContact>> GetAllContactsAsync(bool includeDeletedRecords = false);
		Task<string> CreateContactAsync(CreateContactRequest createContactRequest);
		Task<string> CreateContactClientsAsync(LinkClientsToContact linkClientsToContact);
		Task<LinkClientsToContact> GetContactClients(int contactId);
    }
}