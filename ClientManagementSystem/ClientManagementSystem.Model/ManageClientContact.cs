namespace ClientManagementSystem.Model
{
    public class ManageClientContact
    {
	    public string ClientCode { get; set; }
	    public string Name { get; set; }
	    public DateTime DateOfRecord { get; set; }
	    public int NumberOfContacts { get; set; }
		public List<ListOfContacts> Contacts { get; set; }
	}
}