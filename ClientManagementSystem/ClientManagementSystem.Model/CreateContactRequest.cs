using System.ComponentModel.DataAnnotations;

namespace ClientManagementSystem.Model
{
    public class CreateContactRequest
    {
		[Required]
	    public string Name { get; set; }
	    [Required]
		public string Surname { get; set; }

	    [Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
	}
}
