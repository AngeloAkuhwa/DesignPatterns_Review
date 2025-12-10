using Patterns.Creational.Factory.Payments.Enums;

namespace Patterns.Creational.Factory.Payments.Factories;

public static class PaymentFactorySelector
{
	public static PaymentFactory GetFactory(PaymentProvider provider)
	{
		return provider switch
		{
			PaymentProvider.PayStack => new PayStackFactory(),
			PaymentProvider.FlutterWave => new FlutterWaveFactory(),
			PaymentProvider.Stripe => new StripeFactory(),
			PaymentProvider.BankTransfer => new BankTransferFactory(),
			_ => throw new ArgumentOutOfRangeException(nameof(provider), provider, "Invalid payment provider.")
		};
	}
}