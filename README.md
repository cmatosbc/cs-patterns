
```markdown
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
```

## Adapter Pattern

The Adapter pattern is used to allow incompatible interfaces to work together. It acts as a bridge between two incompatible interfaces.

### Example

```csharp
public interface ITarget
{
    void Request();
}

public class Adaptee
{
    public void SpecificRequest()
    {
        Console.WriteLine("Specific request.");
    }
}

public class Adapter : ITarget
{
    private readonly Adaptee _adaptee;

    public Adapter(Adaptee adaptee)
    {
        _adaptee = adaptee;
    }

    public void Request()
    {
        _adaptee.SpecificRequest();
    }
}
```

## Singleton Pattern

The Singleton pattern ensures that a class has only one instance and provides a global point of access to it.

### Example

```csharp
public class Singleton
{
    private static Singleton _instance;

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }

    public void DoSomething()
    {
        Console.WriteLine("Singleton instance is working.");
    }
}
```

## Factory Pattern

The Factory pattern defines an interface for creating an object but lets subclasses alter the type of objects that will be created.

### Example

```csharp
public abstract class Product
{
    public abstract void DoWork();
}

public class ConcreteProductA : Product
{
    public override void DoWork()
    {
        Console.WriteLine("Product A is working.");
    }
}

public class ConcreteProductB : Product
{
    public override void DoWork()
    {
        Console.WriteLine("Product B is working.");
    }
}

public abstract class Creator
{
    public abstract Product FactoryMethod();

    public void AnOperation()
    {
        var product = FactoryMethod();
        product.DoWork();
    }
}

public class ConcreteCreatorA : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductA();
    }
}

public class ConcreteCreatorB : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductB();
    }
}
```

## NuGet Package

To include this library in your project, you can add it as a NuGet package. Follow these steps to create and publish the package:

1. Create a `.nuspec` file:

```xml
<?xml version="1.0"?>
<package>
  <metadata>
    <id>cs-patterns</id>
    <version>1.0.0</version>
    <title>C# Applied Design Patterns</title>
    <authors>cmatosbc</authors>
    <owners>cmatosbc</owners>
    <licenseUrl>https://opensource.org/licenses/MIT</licenseUrl>
    <projectUrl>https://github.com/cmatosbc/cs-patterns</projectUrl>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>This package provides C# implementations of various design patterns.</description>
    <releaseNotes>Initial release of C# Applied Design Patterns.</releaseNotes>
    <tags>csharp design patterns</tags>
  </metadata>
  <files>
    <file src="bin\Release\netstandard2.0\*.dll" target="lib\netstandard2.0" />
  </files>
</package>
```

2. Pack the project:

```sh
dotnet pack
```

3. Publish the package:

```sh
dotnet nuget push <path-to-nupkg-file> -k <api-key> -s https://api.nuget.org/v3/index.json
```

4. Install the package in your project using the NuGet CLI:

```sh
dotnet add package cs-patterns --version 1.0.0
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or new patterns you'd like to see included.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
```
