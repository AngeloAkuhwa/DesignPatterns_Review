using Patterns.Creational.AbstractFactory.Notifications;
using Patterns.Creational.AbstractFactory.Notifications.Enums;
using Patterns.Creational.Factory.Payments.Enums;
using Patterns.Creational.Factory.Payments.Factories;

namespace Client.Demonstrations;

public static class CreationalDemo
{
	/*
    ===============================================================
    PATTERN: FACTORY METHOD

		DEFINITION:
	   Factory Method is a creational pattern that defines an interface for 
	   creating an object while allowing subclasses to choose which concrete 
	   class to instantiate. It moves object creation logic into separate 
	   factory classes, keeping client code decoupled from concrete types.

	   REAL-WORLD CONTEXT (IN THIS PROJECT):
	   The payment system uses Factory Method to choose the correct payment 
	   processor (PayStack, FlutterWave, Stripe, Bank Transfer) without 
	   exposing concrete processor classes to the client code.

    PROBLEM BEING SOLVED:
        In a payment system, different payment providers (PayStack,
        FlutterWave, Stripe, Bank Transfer) require different
        processing logic. Without a Factory Method, the client code
        would be full of switch cases or "new" statements, tightly
        coupling the system to concrete classes.

    HOW THE PATTERN SOLVES IT:
        Factory Method defines a common creator (PaymentFactory) and
        lets subclasses decide which concrete payment processor to
        create. This removes conditionals from client code and
        allows adding new payment providers without modifying existing
        logic.

    REAL-WORLD BENEFIT:
        - Cleaner architecture
        - Easy extension when adding new gateways
        - Client depends only on abstractions (IPaymentProcessor)
    ===============================================================
*/
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

	/*
    ===============================================================
    PATTERN: ABSTRACT FACTORY

		DEFINITION:
	   Abstract Factory is a creational pattern that provides an interface 
	   for creating entire families of related objects without specifying 
	   their concrete implementations. It guarantees that created products 
	   are compatible and belong to the same group or environment.

	   REAL-WORLD CONTEXT (IN THIS PROJECT):
	   The notification system uses Abstract Factory to produce consistent 
	   sets of services—Email, SMS, Push—based on the selected environment 
	   (Production or Sandbox), ensuring the correct family of providers is 
	   always used together.

    PROBLEM BEING SOLVED:
        A notification system must support different environments
        (Production and Sandbox), each with its own family of
        services: Email, SMS, and Push providers. Without Abstract
        Factory, we would mix concrete classes everywhere and risk
        pairing the wrong services together.

    HOW THE PATTERN SOLVES IT:
        Abstract Factory creates entire "families" of related objects
        without exposing concrete implementations. Each environment
        has its own factory that guarantees consistent combinations:
        - Prod factory → ProdEmail, ProdSms, ProdPush
        - Sandbox factory → SandboxEmail, SandboxSms, SandboxPush

    REAL-WORLD BENEFIT:
        - Environment switching with one line
        - No leaking of concrete classes into business logic
        - Guarantees correct grouping of related services
    ===============================================================
*/
	public static void RunAbstractFactoryDemo()
	{
		while (true)
		{
			Console.Clear();
			WriteInfo("\n=== ABSTRACT FACTORY: NOTIFICATION SYSTEM DEMO ===");

			//SELECT ENVIRONMENT
			WriteInfo("Select environment:");
			Console.WriteLine("1 - Production");
			Console.WriteLine("2 - Sandbox");
			Console.WriteLine("0 - Exit");

			WritePrompt("Choice: ");
			var envInput = Console.ReadLine();

			if (envInput == "0")
			{
				WriteSuccess("Exiting notification demo...");
				return;
			}

			if (!int.TryParse(envInput, out int envValue) || !Enum.IsDefined(typeof(EnvironmentType), envValue))
			{
				WriteError("Invalid environment selection. Try again.\n");
				Pause();
				continue;
			}

			var environment = (EnvironmentType)envValue;
			var factory = NotificationFactorySelector.GetFactory(environment);

			// Build services
			var email = factory.CreateEmailService();
			var sms = factory.CreateSmsService();
			var push = factory.CreatePushService();

			//SELECT NOTIFICATION TYPE
			while (true)
			{
				Console.Clear();
				WriteInfo($"=== {environment.ToString().ToUpper()} MODE SELECTED ===");
				WriteInfo("Choose a notification type:");

				Console.WriteLine("1 - Email Notification");
				Console.WriteLine("2 - SMS Notification");
				Console.WriteLine("3 - Push Notification");
				Console.WriteLine("4 - Send All");
				Console.WriteLine("0 - Back");

				WritePrompt("Choice: ");
				var notifyInput = Console.ReadLine();

				if (notifyInput == "0")
				{
					break;
				}

				if (!int.TryParse(notifyInput, out int notifyValue) || notifyValue is < 1 or > 4)
				{
					WriteError("Invalid notification type. Try again.\n");
					Pause();
					continue;
				}

				Console.Clear();

				var type = (NotificationType)notifyValue;
				// SEND SELECTED NOTIFICATIONS
				switch (type)
				{
					case NotificationType.Email:
						WriteSuccess(email.SendEmail("test@example.com", "Hello from Abstract Factory!"));
						break;

					case NotificationType.Sms:
						WriteSuccess(sms.SendSms("+2348012345678", "Your OTP is 1234"));
						break;

					case NotificationType.PushNotification:
						WriteSuccess(push.SendPush("device-010101", "New message received"));
						break;

					case NotificationType.All:
						WriteSuccess(email.SendEmail("test@example.com", "Hello from Abstract Factory!"));
						WriteSuccess(sms.SendSms("+2348012345678", "Your OTP is 1234"));
						WriteSuccess(push.SendPush("device-010101", "New message received"));
						break;
				}

				WriteInfo("\nNotifications sent successfully!");

				WriteInfo("\nPress ENTER to select another notification or type 'exit' to quit.");
				var cont = Console.ReadLine();

				if (cont?.Trim().ToLower() == "exit")
				{
					WriteSuccess("Goodbye!");
					return;
				}
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
