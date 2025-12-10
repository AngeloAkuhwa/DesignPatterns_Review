using Patterns.Creational.AbstractFactory.Notifications.Interfaces;

namespace Patterns.Creational.AbstractFactory.Notifications.Sandbox;

public class SandboxPushService : IPushService
{
	public string SendPush(string deviceId, string message) => $"[SANDBOX] Simulated push to device {deviceId}: {message}";
}