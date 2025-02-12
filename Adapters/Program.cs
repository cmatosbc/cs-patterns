// File: Adapters/Examples/Program.cs
class Programm
{
    static void Mmain(string[] args)
    {
        Adaptee adaptee = new Adaptee();
        IAdapter adapter = new ConcreteAdapter(adaptee);

        adapter.Request();  // Output: Called SpecificRequest()
    }
}
