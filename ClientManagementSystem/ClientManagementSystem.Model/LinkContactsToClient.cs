using System.ComponentModel.DataAnnotations;

namespace ClientManagementSystem.Model
{
    public class LinkContactsToClient
    {
	    public string ClientCode { get; set; }
	    public string Name { get; set; }
	    [Display(Name = "Date Of Record")]
		public DateTime DateOfRecord { get; set; }
	    [Display(Name = "Linked Contacts")]
		public int NumberOfLinkedContacts { get; set; }
		public List<ListOfContacts> Contacts { get; set; }
	}
}