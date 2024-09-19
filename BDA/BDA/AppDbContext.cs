using Microsoft.EntityFrameworkCore;
using BDA.Models;

namespace BDA
{
	public class AppDbContext:DbContext
	{
		public DbSet<Customers> Customer { get; set; }

		public DbSet<User> Users { get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customers>()
				.HasOne(c => c.CreatedByUser)
				.WithMany(u => u.Customers)
				.HasForeignKey(c => c.CreatedByUserId)
				.OnDelete(DeleteBehavior.Restrict); // This ensures that deleting a User does not delete Customers.
		}

	}
}
