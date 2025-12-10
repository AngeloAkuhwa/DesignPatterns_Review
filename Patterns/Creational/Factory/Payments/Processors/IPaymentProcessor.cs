namespace Patterns.Creational.Factory.Payments.Processors;

/// <summary>
/// Explanation:
/// This is the Product in Factory Method terms
/// All payment processors MUST implement Process(amount)
/// The client code depends ONLY on this interface, not concrete classes.
/// </summary>
public interface IPaymentProcessor
{
    string Process(decimal amount);
}