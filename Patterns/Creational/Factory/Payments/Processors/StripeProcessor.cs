namespace Patterns.Creational.Factory.Payments.Processors;

public class StripeProcessor : IPaymentProcessor
{
    public string Process(decimal amount) => $"Processing ${amount} using Stripe gateway...";
}