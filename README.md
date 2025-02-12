# cs-patterns
C# Applied Design Patterns

## Overview
This repository contains implementations of various design patterns in C#. It serves as a learning resource for developers who want to understand and apply design patterns in their C# projects.

## Table of Contents
- [Service Locator Pattern](#service-locator-pattern)
- [Adapter Pattern](#adapter-pattern)
- [Singleton Pattern](#singleton-pattern)
- [Factory Pattern](#factory-pattern)
- [NuGet Package](#nuget-package)
- [Contributing](#contributing)
- [License](#license)

## Service Locator Pattern

The `Container.cs` file implements a Service Locator pattern. Below are detailed instructions on how to use it.

### Features

- Register services with or without factory methods.
- Support for singleton services.
- Lazy initialization of services.
- Exception handling for service registration and resolution.

### Usage

1. **Define Service Interfaces and Implementations**

```csharp
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
