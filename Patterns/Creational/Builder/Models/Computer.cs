namespace Patterns.Creational.Builder.Models;

public sealed class Computer
{
	public string Cpu { get; init; } = string.Empty;
	public int RamGb { get; init; }
	public int StorageGb { get; init; }
	public string Gpu { get; init; } = "Integrated";
	public bool HasWifi { get; init; }
	public bool HasRgb { get; init; }

	public override string ToString()
	{
		return $"CPU: {Cpu}, RAM: {RamGb}GB, Storage: {StorageGb}GB, GPU: {Gpu}, WiFi: {HasWifi}, RGB: {HasRgb}";
	}
}
