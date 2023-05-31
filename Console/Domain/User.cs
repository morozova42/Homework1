using ConsoleApplication.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
	[Table("users")]
	public class User :BaseEntity
	{
		[Display(Name = "Имя пользователя")]
		public string FirstName { get; set; }

		[Display(Name = "Фамилия пользователя")]
		public string LastName { get; set; }

		[Display(Name = "Отчество пользователя", Description = "необязательное поле")]
		public string? MiddleName { get; set; }
	}
}
