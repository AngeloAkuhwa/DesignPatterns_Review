using Patterns.Creational.AbstractFactory.Notifications.Interfaces;

namespace Patterns.Creational.AbstractFactory.Notifications.Prod;

public class ProdEmailService : IEmailService
{
	public string SendEmail(string to, string message) => $"[PROD] Email sent to {to} via SendGrid: {message}";
}