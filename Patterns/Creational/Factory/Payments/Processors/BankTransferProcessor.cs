using Patterns.Creational.Factory.Payments.Interfaces;

namespace Patterns.Creational.Factory.Payments.Processors;

public class BankTransferProcessor : IPaymentProcessor
{
    public string Process(decimal amount) => $"Processing {amount:C} via Direct Bank Transfer...";
}