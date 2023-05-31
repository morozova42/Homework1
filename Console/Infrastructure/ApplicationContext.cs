using Domain;
using EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Infrastructure
{
	public class ApplicationContext : DbContext
	{
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
			optionsBuilder
				.UseNpgsql(connString)
				.UseLowerCaseNamingConvention();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
			modelBuilder.ApplyConfiguration(new AdvertConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}
