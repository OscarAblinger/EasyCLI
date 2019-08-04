> Important note: This project is heavily WIP – Going so far that a rename is rather likely

# Easy CLI

This library allows you to easily set up a CLI for your project.
Concentrate on making your library or program, and then take only
a few minutes time to make it interactive with this library.

# Motivation

Before making this library, I had to re-create a CLI three times, because
I couldn't find one that would do what I wanted.
The need for it had two different reasons:

- I wanted a quick interface without needing to create a GUI
- I wanted to be able to closely interact and monitor my program
  in order to properly test it.

Obviously, an CLI is perfect for that.
It is a quick GUI that everyone tech-savy can easily interact with, and
you don't even need a minute to add your new method to it, in order to execute
and test it.
Especially in larger project, the last point is a big one, even with a Unit Test
framework.

# Build status & Tests

> Will be added as soon as the library is done

# How to use it

In order to use it, first create an instance of the CLI.

```cs
var cli = EasyCLI.Create();
```

Now you can register some Commands to your CLI-object.
It also has a few basic commands itself, that you can use:

```cs
void echoCmd(EasyCLI cli, IArgumentsInfo argInfo) {
  cli.Out.WriteLine(string.Join(", ", argInfo.Arguments));
}

cli.RegisterCommand(CliCommands.HelpCommand);
cli.RegisterCommand(CliCommands.ExitCommand);
cli.RegisterCommand("echo", echoCmd, "Prints the given string");
```

For more information on Commands and the Arguments class, see
the [Api Reference](#api-reference), and for an even faster setup,
see [Using a configuration Object](#using-a-configuration-object).

Afterwards simply start it:

```cs
cli.Start();
```

## Common customizations

### Changing the Standard input/output

If you want to change the input or output streams from the standard
Console, you can do so in the constructor:

```cs
var cli = EasyCLI.Create(Console.in, Console.out);
```

You can also do that [using a configuration object](#using-a-configuration-object).

### Using a configuration Object

You can also create a configuration object, in which you can add more
options, including a list of Commands:

```cs
var config = new EasyCLI.Configuration() {
  In = Console.In,
  Out = Console.Out,
  Prompt = "> ",
  ExceptionCatchHandler = (ex) => throw ex, // aborts program on any exception
  Commands = new List<Command>()
};

var cli = EasyCLI.Create(config);
```

For a proper setup, you probably want to extract that configuration
in its own class.

```cs
public class Program {
  public static void Main(string[] args)
    => EasyCLI.Create(MyCLI.Config).Run();
}

public class MyCLI {
  public static Configuration Config;

  static MyCLI {
    Config = new Configuration() {
      Greeting = {
        "Welcome to my app!",
        "You can type 'help' to see all commands"
      },
      Commands = {
        CliCommands.HelpCommand,
        CliCommands.ExitCommand,
        EchoCommand,
        SumCommand,
        // CliCommands.Run will automatically create commands the methods
        // given as parameters (see the API reference for more details)
        CliCommands.Run(
          (Func<int, string>)My.StaticMethod,
          (Action<string>)new My().Method
        ),
        // CliCommands.Class will register commands to manage the classes
        // given as parameters (see the API reference for more details)
        CliCommands.Class(
          typeof(My)
        )
      }
    };
  }

  private static Command EchoCommand = new Command(
      names: { "echo" },
      method: EchoCommandMethod,
      description: { "Prints arguments to the output" },
      subCommands: {
        new Command(
          names: { "run" },
          method: EchoRunCommandMethod,
          description: { "Executes the command and prints the result" }
        )
      }
    );

  private static Command SumCommand = new Command(
      names: { "sum" },
      method: SumCommandMethod,
      description : { "Adds any amount of numbers together" },
      longOptions: { "integer-only" },
      // or: options: { new MyOption() }
      shortOptions: {
        { "i", "integer-only", false }, // short cut für integer-only
        { "d", "use-double", true }     // only short command, with name 'use-double'; set by default
      }
    );

  private static void EchoCommandMethod(EasyCLI cli, IArgumentsInfo argInfo) {
    cli.Out.WriteLine(string.Join(", ", argInfo.Arguments));
  }

  private static void EchoRunCommandMethod(EasyCLI cli, IArgumentsInfo argInfo) {
    var cmdResult = cli.RunCommand(String.Join(" ", argInfo.Skip(2)));
    string cmdResultStr = (cmdResult.returnValue.HasValue)
                            ? cmdResult.returnValue.Value.ToString()
                            : "no result";

    cli.Out.WriteLine($"Command returned with {cmdResultStr}.");
  }

  private static double SumCommandMethod(EasyCLI cli, IArgumentsInfo argInfo) {
    double sum = 0.0;

    if (argInfo.options["integer-only"])
      sum = SumAs<int>(argInfo);
    else if (argInfo.options["use-double"])
      sum = SumAs<double>(argInfo);
    else 
      sum = SumAs<float>(argInfo);

    cli.Out.WriteLine($"The sum is: {sum}");
  }

  private static T SumAs<T>(IArgumentsInfo argInfo) {
    T sum = default(T);
    for (int i = 0; i < argInfo.Arguments.Length; ++i) {
      sum += argInfo.GetAsStruct<T>(i).GetValueOrDefault();
    }
    return sum;
  }
}
```

# Screenshots

> Will be added as soon as the library is done

# API Reference

> Here will be an extensive list of the public API

# License

MIT License

Copyright (c) 2019 Oscar Ablinger

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
