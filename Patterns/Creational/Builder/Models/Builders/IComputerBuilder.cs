using Patterns.Creational.Builder.Models;

namespace Patterns.Creational.Builder.Builders;

public interface IComputerBuilder
{
	IComputerBuilder Reset();

	IComputerBuilder SetCpu(string cpu);
	IComputerBuilder SetRam(int gb);
	IComputerBuilder SetStorage(int gb);
	IComputerBuilder SetGpu(string gpu);
	IComputerBuilder EnableWifi();
	IComputerBuilder EnableRgb();

	Computer Build();
}
