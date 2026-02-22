namespace ClientManagementSystem.Data.Entities;

public class Client
{
	public string ClientCode { get; set; }
	public string Name { get; set; }
	public DateTime DateOfRecord { get; set; }
	public DateTime DateModified { get; set; }
}