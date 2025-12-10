using Patterns.Creational.Factory.Payments.Interfaces;
using Patterns.Creational.Factory.Payments.Processors;

namespace Patterns.Creational.Factory.Payments.Factories;

public class StripeFactory : PaymentFactory
{
	public override IPaymentProcessor CreateProcessor() => new StripeProcessor();
}