namespace Patterns.Creational.Factory.Payments.Processors;

public class PayStackProcessor : IPaymentProcessor
{
    public string Process(decimal amount) => $"Processing ₦{amount} using PayStack gateway...";
}