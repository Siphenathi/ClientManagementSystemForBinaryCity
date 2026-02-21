using System.ComponentModel.DataAnnotations;

namespace ClientManagementSystem.Model
{
	public class CreateClientRequest
	{
		public int ClientCode { get; set; }
		[Required]
		public string Name { get; set; }
		public DateTime DateOfRecord { get; set; }
	}
}