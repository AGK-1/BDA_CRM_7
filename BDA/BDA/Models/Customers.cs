using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BDA.Models
{
	public class Customers
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }


		[StringLength(100)]
		public string Surname { get; set; }

		public string PhoneNumber { get; set; }

		[StringLength(100)]
		public string Email { get; set; }

		[StringLength(100)]
		public string Company { get; set; }

		[StringLength(100)]
		public string Department { get; set; }

		[StringLength(100)]
		public string Position { get; set; }

		public int? CreatedByUserId { get; set; } // Nullable in case no user is linked

		public DateTime CreatedAt { get; set; }

		[ForeignKey("CreatedByUserId")]
		public User? CreatedByUser { get; set; }
	}
}
