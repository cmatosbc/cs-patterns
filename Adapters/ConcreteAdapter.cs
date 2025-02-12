// File: Adapters/Examples/ConcreteAdapter.cs
public class ConcreteAdapter : AdapterBase
{
    private readonly Adaptee _adaptee;

    public ConcreteAdapter(Adaptee adaptee)
    {
        _adaptee = adaptee;
    }

    public override void Request()
    {
        _adaptee.SpecificRequest();
    }
}
