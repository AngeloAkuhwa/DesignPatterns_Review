namespace Patterns.Creational.Prototype.Models;

/// <summary>
/// The key thing here is the explicit Clone() method with deep copy.
/// </summary>
public sealed class EmailTemplate
{
	public string Name { get; set; } = string.Empty;   // just a label (e.g., "WelcomeEmail")
	public string Subject { get; set; } = string.Empty;
	public string Body { get; set; } = string.Empty;

	public EmailBranding Branding { get; set; } = new();
	public List<string> Tags { get; set; } = new();

	/// <summary>
	/// Recommended prototype style:
	/// Explicit, strongly-typed deep clone.
	/// </summary>
	public EmailTemplate Clone()
	{
		return new EmailTemplate
		{
			Name = Name,
			Subject = Subject,
			Body = Body,

			// Deep copy nested reference
			Branding = new EmailBranding
			{
				CompanyName = Branding.CompanyName,
				Footer = Branding.Footer
			},

			// Deep copy collection
			Tags = new List<string>(Tags)
		};
	}

	public override string ToString()
	{
		return
				$"Template: {Name}\n" +
				$"Subject: {Subject}\n" +
				$"Brand: {Branding.CompanyName}\n" +
				$"Footer: {Branding.Footer}\n" +
				$"Tags: {(Tags.Count == 0 ? "-" : string.Join(", ", Tags))}\n" +
				$"Body:\n{Body}";
	}
}

public sealed class EmailBranding
{
	public string CompanyName { get; set; } = string.Empty;
	public string Footer { get; set; } = string.Empty;
}
