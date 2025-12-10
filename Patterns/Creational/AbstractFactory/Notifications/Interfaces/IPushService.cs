namespace Patterns.Creational.AbstractFactory.Notifications.Interfaces;

public interface IPushService
{
	string SendPush(string deviceId, string message);
}