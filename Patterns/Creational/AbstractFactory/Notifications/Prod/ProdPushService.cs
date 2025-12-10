using Patterns.Creational.AbstractFactory.Notifications.Interfaces;

namespace Patterns.Creational.AbstractFactory.Notifications.Prod;

public class ProdPushService : IPushService
{
	public string SendPush(string deviceId, string message) => $"[PROD] Push sent to device {deviceId} using Firebase: {message}";
}