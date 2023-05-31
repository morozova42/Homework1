using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Infrastructure
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Recipe> Recipes { get; set; }
		public ApplicationContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connString = $"Host={ConfigurationManager.AppSettings["Host"]};" +
				$"Port={ConfigurationManager.AppSettings["Port"]};" +
				$"Database={ConfigurationManager.AppSettings["Database"]};" +
				$"Username={ConfigurationManager.AppSettings["Username"]};" +
				$"Password={ConfigurationManager.AppSettings["Password"]}";
			optionsBuilder.UseNpgsql(connString);
		}
	}
}
