using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityTypeConfigurations
{
	internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("category");
			builder.HasKey(category => category.Id);
			builder.HasIndex(category => category.Id).IsUnique();
			builder.Property(category => category.Title).HasMaxLength(100);
		}
	}
}
