using Domain.Enums;

namespace Domain
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public Measure Measure { get; set; }
		public Category Category { get; set; }
	}
}