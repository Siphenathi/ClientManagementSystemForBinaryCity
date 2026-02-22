namespace ClientManagementSystem.Model
{
    public class ListOfContacts
    {
	    public int ContactId { get; set; }
	    public string Name { get; set; }
	    public string Surname { get; set; }
	    public string Email { get; set; }
		public bool Linked { get; set; }
	}
}
