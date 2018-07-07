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
						
						Config.TextWriter.WriteLine(a.Value);
					},
					OptionSet = new OptionSet
					{
						new Option("r|repeat", "The amount of times to repeat the value.")
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
