namespace Patterns.Creational.Factory.Payments.Processors
{
    internal class FlutterWaveProcessor : IPaymentProcessor
    {
        public string Process(decimal amount) => $"Processing ₦{amount} using FlutterWave gateway...";
    }
}
