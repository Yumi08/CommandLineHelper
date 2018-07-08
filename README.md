# CommandLineHelper
A simple console helper with commands, options, and parameters.

## Getting Started
There are three classes you are going to want to know about:<br>
The **CommandMaster**, the **CommandSet**, and the **Command**.

#### CommandMaster
The command master inherits from a list of CommandSets, simply put. And can be initialized with a collection initializer with command sets. This is what we'll be using to manage all command sets, and is not completely necessary if you don't want to use it.

#### CommandSet
The command set also inherits from a list, this time of Commands. It's used to manage all commands, and is what runs the commands for you.

#### Command
The command is a class with two important features. Its Name property is the first, and its Run property is the second. The name is what will be used when the command set tries to call it. And the run is what will be ran upon its call. When defining the run property, one argument will be given, the command context. This contains info about the command call and options/arguments given.

## Usage
An example of how to use the CommandMaster and its subclasses is shown below.

```csharp
static void Main()
{
	var master = new CommandMaster
	{
		new CommandSet("main")
		{
			new Command("run", "Runs a program.")
			{
				Run = c =>
				{
					switch (c.Value.ToLower())
					{
						case "chrome":
							Process.Start("chrome.exe");
							break;

						default:
							Console.WriteLine("Unkown command!");
							break;
					}
				}
			}
		},
		new CommandSet("debug")
		{
			new Command("echo", "Echoes value passed in.")
			{
				Run = c =>
				{
					Console.WriteLine(c.Value);

					if (c.OneShotEnabled("t|twice"))
						Console.WriteLine(c.Value);
				},
				OptionSet = new OptionSet()
				{
					new Option("t|twice")
				}
			}
		}
	};

	while (true)
	{
		master.ParseAndRun(Console.ReadLine());
	}
}
```

In order to call these commands, you first write the command set name, and then the command name, and then any parameters or values, and finally any options and arguments.

Writing ``main run chrome`` to the console will open chrome on your system.<br>
Writing ``main`` by itself will result in the message ``Unknown command! Please type 'help' for command details.``<br>
Writing ``main help`` will output a list of all commands contained in the command set "main".<br>
Writing ``debug echo Test Message`` will output ``Test Message``.<br>
Finally, Writing ``debug echo Test Message -t`` or ``debug echo Test Message -twice`` will both print out ``Test Message`` twice.<br>

Please note that anywhere - is used, -- may also be used for options and arguments.
