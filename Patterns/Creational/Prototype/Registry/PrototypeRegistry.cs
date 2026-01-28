using Patterns.Creational.Prototype.Models;

namespace Patterns.Creational.Prototype.Registry;

/// <summary>
/// Registry stores baseline prototypes and creates clones on demand.
/// Think: "shelf of templates" => e.g., WelcomeEmail, PasswordResetEmail, etc.
/// </summary>
public sealed class PrototypeRegistry
{
	private readonly Dictionary<string, EmailTemplate> _prototypes = new(StringComparer.OrdinalIgnoreCase);

	public void Register(string key, EmailTemplate prototype)
	{
		if (string.IsNullOrWhiteSpace(key))
		{
			throw new ArgumentException("Key cannot be empty.", nameof(key));
		}

		_prototypes[key] = prototype;
	}

	public EmailTemplate CreateClone(string key)
	{
		if (!_prototypes.TryGetValue(key, out var prototype))
		{
			throw new KeyNotFoundException($"Prototype '{key}' not found.");
		}

		return prototype.Clone();
	}

	public bool Contains(string key) => _prototypes.ContainsKey(key);
}
