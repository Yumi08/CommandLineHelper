using System;
using CommandLineHelper;

namespace Tests
{
	class Program
	{
		static void Main()
		{
			var set = new CommandSet
			{
				new Command("echo", "Repeats whatever value is passed in.")
				{
					Run = a =>
					{
						var output = a.Value;
						if (a.OneShotEnabled("e|exclaim"))
							output += "!";

						for (var i = 0; i < Convert.ToInt32(a.GetOptionParam("r|repeat")); i++)
							Config.TextWriter.WriteLine(output);
					},
					OptionSet = new OptionSet()
					{
						new Option("r|repeat", "TIMES", "The amount of times to repeat the value.")
						{
							Default = "1",
							Optional = true
						},
						new Option("e|exclaim", "Add an exclamation point in the end.")
					}
				},
				new Command("add", "Add two numbers")
				{
					Run = a =>
					{

						Config.TextWriter.WriteLine(
							Convert.ToInt32(a.GetOptionParam("n1|number-1")) +
							Convert.ToInt32(a.GetOptionParam("n2|number-2"))
							);
					},
					OptionSet = new OptionSet()
					{
						new Option("n1|number-1", "NUMBER", "The first number to be added.")
						{
							Optional = false
						},
						new Option("n2|number-2", "NUMBER", "The second number to be added.")
						{
							Optional = false
						}
					}
				}
			};

			while (true)
			{
				var result = Parser.Parse(Console.ReadLine());

				set.Run(result);
			}
		}
	}
}
