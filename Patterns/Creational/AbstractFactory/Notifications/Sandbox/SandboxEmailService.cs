using Patterns.Creational.AbstractFactory.Notifications.Interfaces;

namespace Patterns.Creational.AbstractFactory.Notifications.Sandbox;

public class SandboxEmailService : IEmailService
{
	public string SendEmail(string to, string message) => $"[SANDBOX] Simulated email to {to}: {message}";
}