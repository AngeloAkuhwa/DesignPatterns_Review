using Client.Models;

namespace Client.Services;

public static class ConsoleExplanationService
{
	public static void PrintExplanation(PatternExplanation exp)
	{
		Console.Clear();
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine($"\n=== {exp.Title.ToUpper()} ===\n");
		Console.ResetColor();

		PrintSection("A. REAL-WORLD PROBLEM", exp.Problem);
		PrintSection("B. INTENT OF THE PATTERN", exp.Intent);
		PrintSection("C. COMPONENTS (Pattern Structure)", exp.Components);
		PrintSection("D. UML BREAKDOWN", exp.Uml);
		PrintSection("E. SUMMARY OF THIS IMPLEMENTATION", exp.Summary);

		Console.ForegroundColor = ConsoleColor.DarkYellow;
		Console.WriteLine("\nPress ENTER to return to menu...");
		Console.ResetColor();
		Console.ReadLine();
	}

	private static void PrintSection(string title, string content)
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"\n{title}");
		Console.ResetColor();

		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine("------------------------------------------");
		Console.WriteLine(content);
		Console.ResetColor();
	}
}