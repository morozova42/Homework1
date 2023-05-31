using ConsoleApplication.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
	[Table("advert")]
	public class Advert : BaseEntity
	{
		public string Title { get; set; }
		public string? Description { get; set; }
		public int Price { get; set; }
		public DateTime CreateDate { get; set; }
		public Category Category { get; set; }
		public User User { get; set; }
	}
}
