using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityTypeConfigurations
{
	internal class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("users");
			builder.HasKey(user => user.Id);
			builder.HasIndex(user => user.Id).IsUnique();
			builder.Property(user => user.FirstName).HasMaxLength(250);
			builder.Property(user => user.LastName).HasMaxLength(250);
			builder.Property(user => user.MiddleName).HasMaxLength(250);
		}
	}
}
