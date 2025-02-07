using System;
using System.Collections.Generic;

/// <summary>
/// A generic interface to represent a service.
/// </summary>
public interface IService { }

/// <summary>
/// A singleton Service Locator class to manage service registrations and resolutions.
/// </summary>
public class ServiceLocator
{
    private static readonly Lazy<ServiceLocator> _instance = new Lazy<ServiceLocator>(() => new ServiceLocator());

    private readonly Dictionary<Type, Lazy<IService>> _services = new Dictionary<Type, Lazy<IService>>();
    private readonly Dictionary<Type, IService> _singletons = new Dictionary<Type, IService>();

    /// <summary>
    /// Gets the singleton instance of the ServiceLocator.
    /// </summary>
    public static ServiceLocator Instance => _instance.Value;

    private ServiceLocator() { }

    /// <summary>
    /// Registers a service type with the locator.
    /// </summary>
    /// <typeparam name="T">The type of the service to register.</typeparam>
    /// <param name="implementation">The implementation of the service.</param>
    public void Register<T>(IService implementation) where T : IService
    {
        var type = typeof(T);
        if (_services.ContainsKey(type) || _singletons.ContainsKey(type))
        {
            throw new InvalidOperationException($"Service of type {type} is already registered.");
        }
        _services[type] = new Lazy<IService>(() => implementation);
    }

    /// <summary>
    /// Registers a service type with a factory method.
    /// </summary>
    /// <typeparam name="T">The type of the service to register.</typeparam>
    /// <param name="factory">The factory method to create the service instance.</param>
    public void Register<T>(Func<IService> factory) where T : IService
    {
        var type = typeof(T);
        if (_services.ContainsKey(type) || _singletons.ContainsKey(type))
        {
            throw new InvalidOperationException($"Service of type {type} is already registered.");
        }
        _services[type] = new Lazy<IService>(factory);
    }

    /// <summary>
    /// Registers a singleton service type with the locator.
    /// </summary>
    /// <typeparam name="T">The type of the service to register as a singleton.</typeparam>
    /// <param name="implementation">The singleton implementation of the service.</param>
    public void RegisterSingleton<T>(IService implementation) where T : IService
    {
        var type = typeof(T);
        if (_singletons.ContainsKey(type))
        {
            throw new InvalidOperationException($"Singleton service of type {type} is already registered.");
        }
        _singletons[type] = implementation;
    }

    /// <summary>
    /// Resolves a service type.
    /// </summary>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <returns>The resolved service instance.</returns>
    public T Resolve<T>() where T : IService
    {
        var type = typeof(T);
        if (_singletons.TryGetValue(type, out var singletonService))
        {
            return (T)singletonService;
        }
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service.Value;
        }
        throw new InvalidOperationException($"No service of type {type} is registered.");
    }
}

// Example services
public interface ILogger : IService
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        try
        {
            Console.WriteLine(message);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Logging failed.", ex);
        }
    }
}

public interface IDataAccess : IService
{
    void LoadData();
}

public class DataAccess : IDataAccess
{
    public void LoadData()
    {
        try
        {
            Console.WriteLine("Loading data...");
        }
        catch (Exception ex)
        {
            throw new DataAccessException("Failed to load data.", ex);
        }
    }
}

public class DataAccessException : Exception
{
    public DataAccessException() { }

    public DataAccessException(string message) 
        : base(message) { }

    public DataAccessException(string message, Exception inner) 
        : base(message, inner) { }
}

class Program
{
    static void Main(string[] args)
    {
        var serviceLocator = ServiceLocator.Instance;

        // Register services
        serviceLocator.Register<ILogger>(new ConsoleLogger());
        serviceLocator.Register<IDataAccess>(() => new DataAccess());
        serviceLocator.RegisterSingleton<ILogger>(new ConsoleLogger());

        // Resolve services
        var logger = serviceLocator.Resolve<ILogger>();
        var dataAccess = serviceLocator.Resolve<IDataAccess>();

        // Use services
        try
        {
            logger.Log("This is a log message.");
            dataAccess.LoadData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

