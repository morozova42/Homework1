using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityTypeConfigurations
{
	internal class AdvertConfiguration : IEntityTypeConfiguration<Advert>
	{
		public void Configure(EntityTypeBuilder<Advert> builder)
		{
			builder.ToTable("advert");
			builder.HasOne(advert => advert.Category).WithMany(category => category.Adverts).HasForeignKey("categoryid").IsRequired();
			builder.HasOne(advert => advert.User).WithMany(user => user.Adverts).HasForeignKey("userid").IsRequired();
			builder.HasKey(advert => advert.Id);
			builder.HasIndex(advert => advert.Id).IsUnique();
			builder.Property(advert => advert.Title).HasMaxLength(250);
			builder.Property(advert => advert.Price).HasColumnName("pricerub");
		}
	}
}
