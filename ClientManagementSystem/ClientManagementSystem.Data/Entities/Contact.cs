namespace ClientManagementSystem.Data.Entities;

public class Contact
{
	public int ContactId { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public DateTime DateOfRecord { get; set; }
	public int NumberOfClients { get; set; }
}