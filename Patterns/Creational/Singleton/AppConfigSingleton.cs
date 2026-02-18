namespace Patterns.Creational.Singleton;

// Thread safe, lazy Singleton
public sealed class AppConfigSingleton
{
	// Lazy ensures thread safe lazy init without extra locks in your code
	private static readonly Lazy<AppConfigSingleton> _instance =
			new(() => new AppConfigSingleton());

	public static AppConfigSingleton Instance => _instance.Value;

	public string EnvironmentName { get; private set; } = "Development";
	public string BaseUrl { get; private set; } = "https://localhost";

	// Private ctor prevents "new AppConfigSingleton()"
	private AppConfigSingleton() { }

	public void Initialize(string environmentName, string baseUrl)
	{
		EnvironmentName = environmentName;
		BaseUrl = baseUrl;
	}
}
