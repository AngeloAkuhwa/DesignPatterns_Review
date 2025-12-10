using Patterns.Creational.Factory.Payments.Processors;

namespace Patterns.Creational.Factory.Payments.Factories;

public class PayStackFactory : PaymentFactory
{
	protected override IPaymentProcessor CreateProcessor() => new PayStackProcessor();
}