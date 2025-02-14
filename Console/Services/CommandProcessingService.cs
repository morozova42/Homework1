﻿using Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace ConsoleApplication.Services
{
	/// <summary>
	/// Сервис обработки пользовательских команд
	/// </summary>
	internal class CommandProcessingService
	{
		public delegate string Callback(string tip);

		private static string _helpMessage;
		private static MethodInfo[] _commands;
		private static DbService _dbService;

		#region Properties
		/// <summary>
		/// Поддерживаемые команды
		/// </summary>
		static MethodInfo[] Commands
		{
			get
			{
				if (_commands == null)
				{
					Type t = typeof(CommandProcessingService);
					_commands = t.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
						.Where(m => !m.IsSpecialName
							&& m.ReturnParameter.ParameterType == typeof(string))
						.ToArray();
				}
				return _commands;
			}
		}

		/// <summary>
		/// Информационное сообщение
		/// </summary>
		static string HelpMessage
		{
			get
			{
				if (string.IsNullOrEmpty(_helpMessage))
				{
					StringBuilder strB = new StringBuilder();
					var maxLettersCount = Commands.Max(m => m.Name.Length);
					strB.Append(" ");
					strB.AppendJoin($",{Environment.NewLine} ", Commands.Select(m => $"{m.Name.PadRight(maxLettersCount, ' ')}\t{GetAttr(m)?.Description}"));
					strB.AppendLine($"{Environment.NewLine} Для выхода нажмите 'q'");
					_helpMessage = strB.ToString();
				}
				return _helpMessage;
			}
		}

		/// <summary>
		/// Сервис работы с репозиториями
		/// </summary>
		static DbService DbService
		{
			get
			{
				if (_dbService == null)
				{
					_dbService = new DbService();
				}
				return _dbService;
			}
		}
		#endregion

		/// <summary>
		/// Обрабатывает поступившую команду
		/// </summary>
		/// <param name="command">Пользовательский ввод</param>
		/// <returns>Строку для вывода</returns>
		public static string ProcessCommand(string command, Callback callback)
		{
			if (!IsCorrectCommand(command))
				return "Неизвестная команда. Введите 'help' для подсказки";
			if (command.ToLower() == "help")
				return HelpMessage;

			MethodInfo commandMethod = Commands.FirstOrDefault(m => m.Name.Equals(command, StringComparison.InvariantCultureIgnoreCase));
			if (commandMethod == null)
				return "Something gone wrong";
			if (commandMethod.Name.StartsWith("create", StringComparison.InvariantCultureIgnoreCase))
			{
				var additionalCommand = string.Empty;
				Dictionary<string, string> entityProperties = new Dictionary<string, string>();
				Type t = Assembly.GetEntryAssembly().ExportedTypes.FirstOrDefault(type => type.Name == commandMethod.Name.Replace("Create", ""));
				PropertyInfo[] props = t?.GetProperties().Where(p => p.Name != "Id").ToArray();

				for (int i = 0; additionalCommand.ToLower() != "q" && i < props.Length; i++)
				{
					var propAttr = (DisplayAttribute)props[i].GetCustomAttributes(typeof(DisplayAttribute)).FirstOrDefault();
					var propName = propAttr?.Name;
					if (propName == null)
					{
						entityProperties.TryAdd(props[i].Name, "");
						continue;
					}
					var propDesc = propAttr?.Description;
					additionalCommand = callback($"Укажите {propName} {(string.IsNullOrEmpty(propDesc) ? "" : "(" + propDesc + ")")}:");
					entityProperties.TryAdd(props[i].Name, additionalCommand);
				}
				if (additionalCommand.ToLower() == "q")
				{
					return "Создание прервано";
				}
				return commandMethod.Invoke(null, new object[] { entityProperties }).ToString();
			}
			return commandMethod.Invoke(null, null).ToString();
		}

		/// <summary>
		/// Получает атрибут <see cref="DisplayAttribute">DisplayAttribute</see> для описания команды
		/// </summary>
		/// <param name="method">Метод</param>
		/// <returns>Найденный атрибут или null</returns>
		private static DisplayAttribute GetAttr(MethodInfo method)
		{
			return (DisplayAttribute)method.GetCustomAttributes(typeof(DisplayAttribute)).FirstOrDefault();
		}

		/// <summary>
		/// Проверяет, известна ли команда
		/// </summary>
		/// <param name="command">Пользовательский ввод</param>
		/// <returns>true, если команда известна</returns>
		public static bool IsCorrectCommand(string command)
		{
			return string.IsNullOrEmpty(command)
				|| Commands.Any(m => m.Name.Equals(command, StringComparison.InvariantCultureIgnoreCase))
					|| command.Equals("help", StringComparison.InvariantCultureIgnoreCase);
		}

		public static void DisposeContext()
		{
			_dbService.Dispose();
		}

		#region Command Methods

		[Display(Description = "Получить список всех объявлений")]
		private static string Adverts()
		{
			StringBuilder strB = new StringBuilder();
			foreach (var adv in DbService.GetAllAdverts().OrderByDescending(a => a.CreateDate))
			{
				strB.AppendLine($" [{adv.Id}] - {adv.Title}:");
				strB.AppendLine($" {adv.Description}");
				strB.AppendLine("***");
				strB.AppendLine($" Разместил пользователь {adv.User.FirstName} {adv.User.LastName} в категории {adv.Category.Title}");
				strB.AppendLine("--------------------------------------------------");
			}
			return strB.ToString();
		}

		[Display(Description = "Получить список всех пользователей")]
		private static string Users()
		{
			StringBuilder strB = new StringBuilder();
			foreach (var user in DbService.GetAllUsers().OrderBy(u => u.LastName))
			{
				strB.AppendLine($" [{user.Id}] - {user.LastName} {user.FirstName} {user.MiddleName}");
			}
			return strB.ToString();
		}

		[Display(Description = "Просмотреть все категории объявлений")]
		private static string Categories()
		{
			StringBuilder strB = new StringBuilder();
			var categories = DbService.GetAllCategories();
			var maxLettersCount = categories.Max(m => m.Title.Length);
			foreach (var cat in categories.OrderBy(u => u.Title))
			{
				strB.AppendLine($" [{cat.Id}] - {cat.Title.PadRight(maxLettersCount, ' ')}\t| {cat.Description}");
			}
			return strB.ToString();
		}

		[Display(Description = "Добавить нового пользователя")]
		private static string CreateUser(Dictionary<string, string> propDict)
		{
			return DbService.CreateUser(propDict) ? "Успешно" : "Не вышло создать 8(";
		}

		[Display(Description = "Добавить новую категорию")]
		private static string CreateCategory(Dictionary<string, string> propDict)
		{
			return DbService.CreateCategory(propDict) ? "Успешно" : "Не вышло создать 8(";
		}
		#endregion

	}
}
