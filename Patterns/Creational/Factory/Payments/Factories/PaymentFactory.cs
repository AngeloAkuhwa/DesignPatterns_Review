using Patterns.Creational.Factory.Payments.Interfaces;

namespace Patterns.Creational.Factory.Payments.Factories;

/// <summary>
/// Explanation:
/// This is the Creator in the Factory Method pattern.
/// It defines the factory method CreateProcessor()
/// It also provides business logic (ProcessPayment) that relies on the product interface.
/// </summary>
public abstract class PaymentFactory
{
    // Factory Method
    protected abstract IPaymentProcessor CreateProcessor();

    // High-level logic that uses the factory method
    public string ProcessPayment(decimal amount)
    {
        var processor = CreateProcessor(); // call factory method
        return processor.Process(amount);
    }
}