using Client.Models;

namespace Client.Explanations;

public static class CreationalPatternExplanations
{
	public static readonly PatternExplanation FactoryMethodExplanation = new PatternExplanation
	{
		Title = "Factory Method Pattern",

		Problem =
@"Imagine you are building a payment system. Different payment providers 
(PayStack, FlutterWave, Stripe, Bank Transfer) all process payments 
differently. If you write code like this:

    if (provider == 'PayStack') new PayStackProcessor();
    else if (provider == 'FlutterWave') new FlutterWaveProcessor();
    else if (provider == 'Stripe') new StripeProcessor();

your code quickly becomes messy and difficult to maintain.

Why?

1. Every time you add a new payment provider, you must MODIFY existing code.
2. The client code is tightly coupled to concrete classes.
3. It violates the Open/Closed Principle: your code should be open for 
   extension but closed for modification.
4. Testing becomes harder, because the client directly depends on 
   payment classes.

In real life, payment providers change often, and you want a clean system 
that allows you to plug in new providers without rewriting old code.",

		Intent =
@"The Factory Method pattern provides a way to delegate object creation 
to subclasses instead of creating objects directly.

In simple terms:
• You ask a factory to give you the right payment processor.
• The factory decides which processor to create.
• The client never uses the 'new' keyword to create processors.

This makes the system flexible, extendable, and easier to maintain.",

		Components =
@"• Creator (PaymentFactory)
    - Defines a method CreateProcessor() but does NOT decide which 
      processor to create.

• Concrete Creators (PayStackFactory, FlutterWaveFactory, StripeFactory...)
    - Each of these decides which processor to create.
    - Example: PayStackFactory returns PayStackProcessor.

• Product Interface (IPaymentProcessor)
    - Defines the behavior shared by all payment processors 
      (e.g., Process(amount)).

• Concrete Products (PayStackProcessor, StripeProcessor, etc.)
    - Actual implementations that contain real payment logic.

• Client (Your console application)
    - Makes a request to the factory.
    - Does NOT know which concrete processor is returned.",

		Uml =
@"Creator (PaymentFactory)
    + CreateProcessor(): IPaymentProcessor

ConcreteFactories (PayStackFactory, StripeFactory, etc.)
    + CreateProcessor(): ConcreteProcessor

Product Interface (IPaymentProcessor)
    + Process(amount)

Concrete Products (PayStackProcessor, FlutterWaveProcessor, etc.)
    + Process(amount)",

		Summary =
@"In this project:
1. The user selects a payment method.
2. PaymentFactorySelector returns the correct factory.
3. The factory creates the right payment processor.
4. The client (console app) only calls ProcessPayment(amount).

The client NEVER knows which concrete class is used — it only works with 
IPaymentProcessor. This perfectly demonstrates the power of Factory Method:
cleaner code, easier testing, and effortless extension."
	};



	public static readonly PatternExplanation AbstractFactoryExplanation = new PatternExplanation
	{
		Title = "Abstract Factory Pattern",

		Problem =
@"Consider a notification system where your app must support two environments:

1. Production (real providers)
    - SendGrid Email
    - Twilio SMS
    - Firebase Push

2. Sandbox (fake providers for testing)
    - FakeEmailService
    - FakeSmsService
    - FakePushService

If you do something like this:

    if (env == 'Prod') new ProdEmailService();
    if (env == 'Prod') new ProdSmsService();
    if (env == 'Sandbox') new FakeEmailService();

…it becomes extremely easy to accidentally mix environments (e.g., 
ProdEmail with FakeSms).  
This leads to inconsistent behavior, bugs, and makes testing very hard.

We need a pattern that guarantees:

• All related services come from the same environment.
• The client never knows concrete classes.
• Switching environments is a single action, not multiple if-statements.",

		Intent =
@"Abstract Factory provides one central factory that creates ENTIRE 
families of related objects.

In this example:
• Production factory creates Production Email, SMS, Push services.
• Sandbox factory creates Sandbox Email, SMS, Push services.

The client chooses ONE factory, and the factory guarantees all services 
are from the same family. The client only interacts with interfaces and 
never concrete classes.",

		Components =
@"• Abstract Factory (INotificationFactory)
    - Defines methods for creating each product type:
        CreateEmailService()
        CreateSmsService()
        CreatePushService()

• Concrete Factories (ProdNotificationFactory, SandboxNotificationFactory)
    - Implement the creation of all related services.
    - Production factory returns production services.
    - Sandbox factory returns sandbox services.

• Abstract Products (IEmailService, ISmsService, IPushService)
    - Define how each notification service should behave.

• Concrete Products (ProdEmailService, SandboxSmsService, etc.)
    - Real implementations of each service.

• Client (Your console demo)
    - Selects environment
    - Selects notification type
    - Sends notifications using abstract interfaces ONLY.",

		Uml =
@"AbstractFactory (INotificationFactory)
    + CreateEmailService()
    + CreateSmsService()
    + CreatePushService()

ConcreteFactory1: Production
    + CreateEmailService() -> ProdEmailService
    + CreateSmsService() -> ProdSmsService
    + CreatePushService() -> ProdPushService

ConcreteFactory2: Sandbox
    + CreateEmailService() -> SandboxEmailService
    + CreateSmsService() -> SandboxSmsService
    + CreatePushService() -> SandboxPushService

Client:
    - Works only with interfaces, not concrete classes.",

		Summary =
@"In this project:
1. User selects an environment (Production or Sandbox).
2. NotificationFactorySelector returns the correct concrete factory.
3. The app creates Email, SMS, or Push services through this factory.
4. Every service created is guaranteed to belong to the same environment.
5. The client never touches concrete classes.

This shows the essence of Abstract Factory:
You create whole families of related objects without ever specifying 
or depending on their concrete implementations."
	};
}
