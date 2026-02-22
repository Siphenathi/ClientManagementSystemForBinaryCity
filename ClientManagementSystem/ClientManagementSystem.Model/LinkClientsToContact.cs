using System.ComponentModel.DataAnnotations;

namespace ClientManagementSystem.Model
{
    public class LinkClientsToContact
    {
		public int ContactId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		[Display(Name = "Date Of Record")]
		public DateTime DateOfRecord { get; set; }
		[Display(Name = "Linked Clients")]
		public int NumberOfLinkedClient { get; set; }
		public List<ListOfClients> Clients { get; set; }
	}
}