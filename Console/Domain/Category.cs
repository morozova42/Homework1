using ConsoleApplication.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
	[Table("category")]
	public class Category : BaseEntity
	{
		public string Title { get; set; }
		public string? Description { get; set; }
		public List<Advert> Adverts { get; set; }
	}
}
