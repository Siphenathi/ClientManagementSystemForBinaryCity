namespace ClientManagementSystem.Data.Entities;

public class ClientContact
{
	public string ClientCode { get; set; }
	public int ContactId { get; set; }
	public bool Deleted { get; set; }
	public DateTime DateOfRecord { get; set; }
	public DateTime DateModified { get; set; }
}