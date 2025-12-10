using Patterns.Creational.Factory.Payments.Enums;
using Patterns.Creational.Factory.Payments.Factories;

namespace Client.Demonstrations;

public static class CreationalDemo
{
	public static void RunFactoryDemo()
	{
		while (true)
		{
			Console.Clear();
			WriteInfo("\n=== PAYMENT FACTORY METHOD DEMO ===");

			WriteInfo("Choose payment gateway:");
			Console.WriteLine("1 - Paystack");
			Console.WriteLine("2 - Flutterwave");
			Console.WriteLine("3 - Stripe");
			Console.WriteLine("4 - Bank Transfer");
			Console.WriteLine("0 - Exit");

			WritePrompt("Choice: ");
			var input = Console.ReadLine();

			if (input == "0")
			{
				WriteSuccess("Exiting payment demo...");
				return;
			}

			if (!int.TryParse(input, out int providerValue) ||
					!Enum.IsDefined(typeof(PaymentProvider), providerValue))
			{
				WriteError("Invalid selection. Please try again.\n");
				Pause();
				continue;
			}

			var provider = (PaymentProvider)providerValue;
			var factory = PaymentFactorySelector.GetFactory(provider);

			WritePrompt("Enter amount: ");
			if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
			{
				WriteError("Invalid amount. Please enter a value greater than zero.\n");
				Pause();
				continue;
			}

			var result = factory.ProcessPayment(amount);

			WriteSuccess(result);
			WriteSuccess("Payment processed successfully!");

			WriteInfo("\nPress ENTER to make another payment or type 'exit' to quit.");
			var next = Console.ReadLine();
			if (next?.Trim().ToLower() == "exit")
			{
				WriteSuccess("Goodbye!");
				return;
			}
		}
	}

	private static void WriteError(string message)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"{Icons.Error} {message}");
		Console.ResetColor();
	}

	private static void WriteSuccess(string message)
	{
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"{Icons.Success} {message}");
		Console.ResetColor();
	}

	private static void WriteInfo(string message)
	{
		Console.ForegroundColor = ConsoleColor.Cyan;
		Console.WriteLine(message);
		Console.ResetColor();
	}

	private static void WritePrompt(string message)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write(message);
		Console.ResetColor();
	}

	private static void Pause()
	{
		Console.ForegroundColor = ConsoleColor.DarkGray;
		Console.Write("Press ENTER to continue...");
		Console.ResetColor();
		Console.ReadLine();
	}

	private static class Icons
	{
		public const string Error = "\u274C";        // ❌
		public const string Success = "\u2714";      // ✔
		public const string Warning = "\u26A0";      // ⚠
		public const string Arrow = "\u279C";        // ➜
		public const string Money = "\U0001F4B0";    // 💰
	}
}
