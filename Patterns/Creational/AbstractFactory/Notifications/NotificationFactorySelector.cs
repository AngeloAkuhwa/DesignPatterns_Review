using Patterns.Creational.AbstractFactory.Notifications.Enums;
using Patterns.Creational.AbstractFactory.Notifications.Interfaces;

namespace Patterns.Creational.AbstractFactory.Notifications;

using Prod;
using Sandbox;

public static class NotificationFactorySelector
{
	public static INotificationFactory GetFactory(EnvironmentType type)
	{
		return type switch
		{
			EnvironmentType.Production => new ProdNotificationFactory(),
			EnvironmentType.Sandbox => new SandboxNotificationFactory(),
			_ => throw new ArgumentOutOfRangeException(nameof(type))
		};
	}
}