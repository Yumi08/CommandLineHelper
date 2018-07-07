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
				new Command
				{
					Name = "echo",
					Run = a => Config.TextWriter.WriteLine(a.Value),
					HelpText = "Writes a value to console."
				},
			};

			while (true)
			{
				var result = Parser.Parse(Console.ReadLine());

				set.Run(result);
			}


		}
	}
}
