using Patterns.Creational.Builder.Models;

namespace Patterns.Creational.Builder.Builders;

public sealed class ComputerBuilder : IComputerBuilder
{
	private string _cpu = string.Empty;
	private int _ramGb;
	private int _storageGb;
	private string _gpu = "Integrated";
	private bool _hasWifi;
	private bool _hasRgb;

	public IComputerBuilder Reset()
	{
		_cpu = string.Empty;
		_ramGb = 0;
		_storageGb = 0;
		_gpu = "Integrated";
		_hasWifi = false;
		_hasRgb = false;
		return this;
	}

	public IComputerBuilder SetCpu(string cpu) { _cpu = cpu; return this; }
	public IComputerBuilder SetRam(int gb) { _ramGb = gb; return this; }
	public IComputerBuilder SetStorage(int gb) { _storageGb = gb; return this; }
	public IComputerBuilder SetGpu(string gpu) { _gpu = gpu; return this; }
	public IComputerBuilder EnableWifi() { _hasWifi = true; return this; }
	public IComputerBuilder EnableRgb() { _hasRgb = true; return this; }

	public Computer Build()
	{
		if (string.IsNullOrWhiteSpace(_cpu))
			throw new InvalidOperationException("CPU is required.");
		if (_ramGb <= 0)
			throw new InvalidOperationException("RAM must be greater than 0.");
		if (_storageGb <= 0)
			throw new InvalidOperationException("Storage must be greater than 0.");

		return new Computer
		{
			Cpu = _cpu,
			RamGb = _ramGb,
			StorageGb = _storageGb,
			Gpu = _gpu,
			HasWifi = _hasWifi,
			HasRgb = _hasRgb
		};
	}
}
