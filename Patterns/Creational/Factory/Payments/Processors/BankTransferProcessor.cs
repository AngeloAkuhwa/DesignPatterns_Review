using Patterns.Creational.Factory.Payments.Interfaces;

namespace Patterns.Creational.Factory.Payments.Processors;

public class BankTransferProcessor : IPaymentProcessor
{
    public string Process(decimal amount) => $"Processing ₦{amount} via Direct Bank Transfer...";
}