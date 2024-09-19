using System.Text.Json.Serialization;
using OfficeOpenXml.Style;

namespace BDA.Models
{
	public class User
	{
		public int Id { get; set; } // Primary Key
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Role { get; set; } // "Super Admin", "Admin", "Agent"
		public DateTime CreatedAt { get; set; }
		public string CreatorUser { get; set; }
		[JsonIgnore] // Prevent circular reference issues
		public List<Customers>? Customers { get; set; }
	}
}
