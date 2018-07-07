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
				new Command("egg", "Prints out egg.")
				{
					Run = c =>
					{
						var output = "egg";
						if (c.OneShotEnabled("t|twice"))
							output += " egg";

						if (c.OneShotEnabled("e|exclaim"))
							output += "!";

						Console.WriteLine(output);
					},
					OptionSet = new OptionSet()
					{
						new Option("t|twice", "Says egg twice."),
						new Option("e|exclaim", "Makes it exclaim egg!")
					}
				},
				new Command("wearing", "Test help text.")
			};

			while (true)
			{
				var input = Parser.Parse(Console.ReadLine());

				set.Run(input);
			}
		}
	}
}
