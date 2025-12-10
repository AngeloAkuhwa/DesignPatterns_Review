using Patterns.Creational.AbstractFactory.Notifications.Interfaces;

namespace Patterns.Creational.AbstractFactory.Notifications.Sandbox;

public class SandboxSmsService : ISmsService
{
	public string SendSms(string phone, string message) => $"[SANDBOX] Simulated SMS to {phone}: {message}";
}