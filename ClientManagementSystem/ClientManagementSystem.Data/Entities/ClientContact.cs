namespace ClientManagementSystem.Data.Entities;

public class ClientContact
{
	public int ClientCode { get; set; }
	public int ContactId { get; set; }
	public bool Active { get; set; }
	public DateTime DateOfRecord { get; set; }
}