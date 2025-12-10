using Patterns.Creational.AbstractFactory.Notifications.Interfaces;

namespace Patterns.Creational.AbstractFactory.Notifications.Prod;

public class ProdSmsService : ISmsService
{
	public string SendSms(string phone, string message) => $"[PROD] SMS sent to {phone} using Twilio: {message}";
}