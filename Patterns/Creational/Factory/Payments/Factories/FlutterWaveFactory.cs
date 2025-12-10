using Patterns.Creational.Factory.Payments.Processors;

namespace Patterns.Creational.Factory.Payments.Factories;

public class FlutterWaveFactory : PaymentFactory
{
	protected override IPaymentProcessor CreateProcessor()=> new FlutterWaveProcessor();
}