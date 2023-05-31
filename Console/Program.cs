using ConsoleApplication.Services;
using Services;

namespace ConsoleApplication
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.BackgroundColor = ConsoleColor.Cyan;
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine("********************");
			Console.WriteLine("* Доска объявлений *");
			Console.WriteLine($"********************{Environment.NewLine}");
			Console.ResetColor();
			Console.CursorVisible = true;
			string userCommand = Console.ReadLine();
			while (userCommand?.ToLower() != "q")
			{
				Beep(userCommand);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(CommandProcessingService.ProcessCommand(userCommand));
				Console.ResetColor();
				userCommand = Console.ReadLine();
			}
		}

		static void Beep(string command)
		{
			if (CommandProcessingService.IsCorrectCommand(command))
				Console.Beep(300, 500); //correct command beep
			else
				Console.Beep(650, 700); //unknown command beep
		}
	}
}