using Patterns.Creational.Factory.Payments.Interfaces;
using Patterns.Creational.Factory.Payments.Processors;

namespace Patterns.Creational.Factory.Payments.Factories;

public class BankTransferFactory : PaymentFactory
{
	protected override IPaymentProcessor CreateProcessor() => new BankTransferProcessor();
}