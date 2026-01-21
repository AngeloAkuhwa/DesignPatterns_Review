using Patterns.Creational.Builder.Builders;
using Patterns.Creational.Builder.Models;

namespace Patterns.Creational.Builder.Director;

public sealed class ComputerDirector
{
	private readonly IComputerBuilder _builder;

	public ComputerDirector(IComputerBuilder builder)
	{
		_builder = builder;
	}

	public Computer BuildOfficePc()
	{
		return _builder.Reset()
				.SetCpu("Intel i5")
				.SetRam(16)
				.SetStorage(512)
				.EnableWifi()
				.Build();
	}

	public Computer BuildGamingPc()
	{
		return _builder.Reset()
				.SetCpu("Ryzen 7")
				.SetRam(32)
				.SetStorage(1000)
				.SetGpu("RTX 4070")
				.EnableWifi()
				.EnableRgb()
				.Build();
	}
}
