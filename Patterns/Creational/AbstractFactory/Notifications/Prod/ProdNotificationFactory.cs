using Patterns.Creational.AbstractFactory.Notifications.Interfaces;

namespace Patterns.Creational.AbstractFactory.Notifications.Prod;

public class ProdNotificationFactory : INotificationFactory
{
	public IEmailService CreateEmailService() => new ProdEmailService();
	public ISmsService CreateSmsService() => new ProdSmsService();
	public IPushService CreatePushService() => new ProdPushService();
}