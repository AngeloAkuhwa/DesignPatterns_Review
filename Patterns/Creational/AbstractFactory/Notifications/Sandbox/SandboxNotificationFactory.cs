using Patterns.Creational.AbstractFactory.Notifications.Interfaces;

namespace Patterns.Creational.AbstractFactory.Notifications.Sandbox;

public class SandboxNotificationFactory : INotificationFactory
{
	public IEmailService CreateEmailService() => new SandboxEmailService();
	public ISmsService CreateSmsService() => new SandboxSmsService();
	public IPushService CreatePushService() => new SandboxPushService();
}