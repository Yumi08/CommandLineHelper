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
					Run = a => Console.WriteLine(a.Value)
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
