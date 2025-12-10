using Patterns.Creational.Factory.Payments.Processors;

namespace Patterns.Creational.Factory.Payments.Factories;

public class StripeFactory : PaymentFactory
{
	protected override IPaymentProcessor CreateProcessor() => new StripeProcessor();
}