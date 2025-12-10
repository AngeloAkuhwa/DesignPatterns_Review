namespace Patterns.Creational.AbstractFactory.Notifications.Interfaces;

public interface INotificationFactory
{
	IEmailService CreateEmailService();
	ISmsService CreateSmsService();
	IPushService CreatePushService();
}