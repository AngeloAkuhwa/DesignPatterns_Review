using Client.Explanations;
using Client.Services;
using Patterns.Creational.AbstractFactory.Notifications;
using Patterns.Creational.AbstractFactory.Notifications.Enums;
using Patterns.Creational.Builder.Builders;
using Patterns.Creational.Builder.Director;
using Patterns.Creational.Factory.Payments.Enums;
using Patterns.Creational.Factory.Payments.Factories;
using Patterns.Creational.Prototype.Models;
using Patterns.Creational.Prototype.Registry;
using Patterns.Creational.Singleton;

namespace Client.Demonstrations;

public static class CreationalDemo
{
	public static void RunSingletonDemo()
	{
		// Same instance everywhere
		var config1 = AppConfigSingleton.Instance;
		config1.Initialize("Production", "https://api.my-saas.com");

		var config2 = AppConfigSingleton.Instance;

		Console.WriteLine($"Config1 Env: {config1.EnvironmentName}, BaseUrl: {config1.BaseUrl}");
		Console.WriteLine($"Config2 Env: {config2.EnvironmentName}, BaseUrl: {config2.BaseUrl}");

		Console.WriteLine("Same instance? " + ReferenceEquals(config1, config2));
	}

	public static void RunFactoryDemo()
	{
		while (true)
		{
			Console.Clear();
			WriteInfo("\n=== PAYMENT FACTORY METHOD DEMO ===");

			WriteInfo("Choose payment gateway:");
			Console.WriteLine("1 - PayStack");
			Console.WriteLine("2 - FlutterWave");
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
				WriteError("Invalid amount. Amount must be greater than zero.\n");
				Pause();
				continue;
			}

			var result = factory.ProcessPayment(amount);
			var transactionId = Guid.NewGuid().ToString("N").Substring(0, 10);
			var timestamp = DateTime.Now;

			WriteSuccess(result);
			WriteSuccess("Payment processed successfully!");

			// POST-PAYMENT OPTIONS
			while (true)
			{
				WriteInfo("\nSelect an option:");
				Console.WriteLine("1 - Print Receipt");
				Console.WriteLine("2 - Print Detailed Breakdown");
				Console.WriteLine("3 - Print Factory Method Pattern Explanation");
				Console.WriteLine("4 - Process Another Payment");
				Console.WriteLine("0 - Exit");

				WritePrompt("Choice: ");
				var option = Console.ReadLine();

				switch (option)
				{
					// Print Receipt
					case "1":
						Console.Clear();
						WriteInfo("=== PAYMENT RECEIPT ===\n");
						WriteSuccess($"Transaction ID: {transactionId}");
						WriteSuccess($"Provider: {provider}");
						WriteSuccess($"Amount: {amount:C}");
						WriteSuccess($"Timestamp: {timestamp}");
						WriteSuccess("Status: Successful");
						Pause();
						break;

					// Print Breakdown
					case "2":
						Console.Clear();
						WriteInfo("=== PAYMENT BREAKDOWN ===\n");
						WriteSuccess($"Factory Used: {factory.GetType().Name}");
						WriteSuccess($"Processor Returned: {factory.CreateProcessor().GetType().Name}");
						WriteSuccess($"Amount Sent: {amount:C}");
						WriteSuccess($"Executed At: {timestamp}");
						WriteInfo("\nThis shows how the Factory Method dynamically selects\n" +
											"a concrete payment processor based on user input\n" +
											"without the client instantiating the class directly.");
						Pause();
						break;

					// Print Explanation
					case "3":
						ConsoleExplanationService.PrintExplanation(CreationalPatternExplanations.FactoryMethodExplanation);
						break;

					// Make another payment
					case "4":
						goto ContinueOuterLoop;

					// Exit entirely
					case "0":
						WriteSuccess("Goodbye!");
						return;

					default:
						WriteError("Invalid option. Try again.");
						Pause();
						break;
				}
			}

			ContinueOuterLoop: continue;
		}
	}

	public static void RunAbstractFactoryDemo()
	{
		while (true)
		{
			Console.Clear();
			WriteInfo("\n=== ABSTRACT FACTORY: NOTIFICATION SYSTEM DEMO ===");

			// SELECT ENVIRONMENT
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

			// SELECT NOTIFICATION TYPE
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
					break;

				if (!int.TryParse(notifyInput, out int notifyValue) ||
						notifyValue is < 1 or > 4)
				{
					WriteError("Invalid notification type. Try again.\n");
					Pause();
					continue;
				}

				Console.Clear();

				var type = (NotificationType)notifyValue;
				var timestamp = DateTime.Now;
				var transactionId = Guid.NewGuid().ToString("N")[..10];

				// SEND SELECTED NOTIFICATIONS

				string? lastMessage = null;

				switch (type)
				{
					case NotificationType.Email:
						lastMessage = email.SendEmail("test@example.com", "Hello from Abstract Factory!");
						WriteSuccess(lastMessage);
						break;

					case NotificationType.Sms:
						lastMessage = sms.SendSms("+2348012345678", "Your OTP is 1234");
						WriteSuccess(lastMessage);
						break;

					case NotificationType.PushNotification:
						lastMessage = push.SendPush("device-010101", "New message received");
						WriteSuccess(lastMessage);
						break;

					case NotificationType.All:
						WriteSuccess(email.SendEmail("test@example.com", "Hello from Abstract Factory!"));
						WriteSuccess(sms.SendSms("+2348012345678", "Your OTP is 1234"));
						lastMessage = push.SendPush("device-010101", "New message received");
						WriteSuccess(lastMessage);
						break;
				}

				WriteInfo("\nNotifications sent successfully!");

				// POST-NOTIFICATION MENU
				while (true)
				{
					WriteInfo("\nSelect an option:");
					Console.WriteLine("1 - Print Notification Receipt");
					Console.WriteLine("2 - Print Detailed Breakdown");
					Console.WriteLine("3 - Print Abstract Factory Explanation");
					Console.WriteLine("4 - Send Another Notification");
					Console.WriteLine("0 - Exit");

					WritePrompt("Choice: ");
					var option = Console.ReadLine();

					switch (option)
					{
						// Receipt
						case "1":
							Console.Clear();
							WriteInfo("=== NOTIFICATION RECEIPT ===\n");
							WriteSuccess($"Environment: {environment}");
							WriteSuccess($"Notification Type: {type}");
							WriteSuccess($"Message: {lastMessage}");
							WriteSuccess($"Timestamp: {timestamp}");
							WriteSuccess($"Reference ID: {transactionId}");
							Pause();
							break;

						// Detailed Breakdown
						case "2":
							Console.Clear();
							WriteInfo("=== NOTIFICATION BREAKDOWN ===\n");
							WriteSuccess($"Factory Used: {factory.GetType().Name}");
							WriteSuccess($"Email Service: {email.GetType().Name}");
							WriteSuccess($"SMS Service: {sms.GetType().Name}");
							WriteSuccess($"Push Service: {push.GetType().Name}");
							WriteSuccess($"Last Notification Sent: {type}");
							WriteSuccess($"Sent At: {timestamp}");
							WriteInfo("\nThis demonstrates the Abstract Factory pattern,");
							WriteInfo("which guarantees families of related objects remain compatible.");
							Pause();
							break;

						// Pattern Explanation
						case "3":
							ConsoleExplanationService.PrintExplanation(
									CreationalPatternExplanations.AbstractFactoryExplanation);
							break;

						// Send another notification
						case "4":
							goto SelectAnotherNotification;

						// Exit all
						case "0":
							WriteSuccess("Goodbye!");
							return;

						default:
							WriteError("Invalid option. Try again.");
							Pause();
							break;
					}
				}

				SelectAnotherNotification: continue;
			}
		}
	}

	public static void RunBuilderDemo()
	{
		var builder = new ComputerBuilder();
		var director = new ComputerDirector(builder);

		var officePc = director.BuildOfficePc();
		var gamingPc = director.BuildGamingPc();

		Console.WriteLine("Office PC:");
		Console.WriteLine(officePc);

		Console.WriteLine();
		Console.WriteLine("Gaming PC:");
		Console.WriteLine(gamingPc);

		Console.WriteLine();
		Console.WriteLine("Custom build (no director):");
		var custom = builder.Reset()
				.SetCpu("Intel i9")
				.SetRam(64)
				.SetStorage(2000)
				.SetGpu("RTX 4090")
				.EnableWifi()
				.Build();

		Console.WriteLine(custom);
	}

	public static void RunPrototypeDemo()
	{
		// Create a baseline template ONCE => this is the "prototype".
		var welcomeTemplate = new EmailTemplate
		{
			Name = "WelcomeEmail",
			Subject = "Welcome to Schedula",
			Body = "Hi {{Name}},\n\nWelcome to Schedula! We’re glad to have you.\n\nCheers,\nTeam",
			Branding = new EmailBranding
			{
				CompanyName = "Schedula Inc",
				Footer = "© 2026 Schedula. All rights reserved."
			},
			Tags = new List<string> { "transactional", "welcome" }
		};

		// Register it
		var registry = new PrototypeRegistry();
		registry.Register("WelcomeEmail", welcomeTemplate);

		// Clone it for different users => copy  and  tweak
		var emailForAngelo = registry.CreateClone("WelcomeEmail");
		emailForAngelo.Subject = "Welcome, Angelo";
		emailForAngelo.Body = emailForAngelo.Body.Replace("{{Name}}", "Angelo");

		var emailForSeyi = registry.CreateClone("WelcomeEmail");
		emailForSeyi.Subject = "Welcome, Seyi";
		emailForSeyi.Body = emailForSeyi.Body.Replace("{{Name}}", "Seyi");

		// Print results
		WriteInfo("=== Email for Angelo ===");
		WriteInfo(emailForAngelo);

		WriteInfo("\n=== Email for Seyi ===");
		WriteInfo(emailForSeyi);

		// Proves deep copy works (changing one clone doesn't affect the other)
		emailForAngelo.Branding.Footer = "Angelo footer test";
		emailForAngelo.Tags.Add("vip");

		WriteInfo("\n=== Deep copy proof ===");
		WriteInfo("Angelo footer: " + emailForAngelo.Branding.Footer);
		WriteInfo("Seyi footer: " + emailForSeyi.Branding.Footer);
		WriteInfo("Angelo tags: " + string.Join(", ", emailForAngelo.Tags));
		WriteInfo("Seyi tags: " + string.Join(", ", emailForSeyi.Tags));
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

	private static void WriteInfo(object message)
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
