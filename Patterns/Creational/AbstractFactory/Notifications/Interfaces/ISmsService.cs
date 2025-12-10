namespace Patterns.Creational.AbstractFactory.Notifications.Interfaces;

public interface ISmsService
{
	string SendSms(string phone, string message);
}