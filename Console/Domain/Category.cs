using ConsoleApplication.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Domain
{
	public class Category : BaseEntity
	{
		[Display(Name = "Название категории")]
		public string Title { get; set; }

		[Display(Name = "Описание категории", Description = "необязательное поле")]
		public string? Description { get; set; }
		public List<Advert> Adverts { get; set; }
	}
}
