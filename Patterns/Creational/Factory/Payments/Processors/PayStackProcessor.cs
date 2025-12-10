using Patterns.Creational.Factory.Payments.Interfaces;

namespace Patterns.Creational.Factory.Payments.Processors;

public class PayStackProcessor : IPaymentProcessor
{
    public string Process(decimal amount) => $"Processing {amount:C} using PayStack gateway...";
}