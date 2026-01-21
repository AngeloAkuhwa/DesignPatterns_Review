using Client.Demonstrations;

namespace Client;

internal static class Program
{
	static void Main(string[] args)
	{
		//// Ensure Unicode icons display correctly
		//Console.OutputEncoding = System.Text.Encoding.UTF8;

		////Factory demo
		//CreationalDemo.RunFactoryDemo();

		////Abstract factory Demo
		//CreationalDemo.RunAbstractFactoryDemo();

		CreationalDemo.RunBuilderDemo();
	}
}