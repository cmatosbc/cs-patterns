// File: Adapters/Examples/Program.cs
class Program
{
    static void Main(string[] args)
    {
        Adaptee adaptee = new Adaptee();
        IAdapter adapter = new ConcreteAdapter(adaptee);

        adapter.Request();  // Output: Called SpecificRequest()
    }
}
