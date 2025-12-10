namespace Patterns.Creational.AbstractFactory.Notifications.Interfaces;

public interface IEmailService
{
	string SendEmail(string to, string message);
}