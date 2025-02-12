using System;

public abstract class SingletonBase<T> where T : SingletonBase<T>, new()
{
    private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

    public static T Instance => _instance.Value;

    protected SingletonBase()
    {
        if (_instance.IsValueCreated)
        {
            throw new InvalidOperationException("Instance already created!");
        }
    }

    public abstract void DoSomething();
}
