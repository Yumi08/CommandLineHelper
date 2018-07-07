using System;
using CommandLineHelper;

namespace Tests
{
	class Program
	{
		static void Main()
		{
			while (true)
			{
				var result = Parser.Parse(Console.ReadLine());

				Console.WriteLine($"COMMAND: {result.Command}");
				foreach (var e in result.Arguments)
				{
					Console.WriteLine($"ARGUMENT: {e.Key}, VALUE: {e.Value}");
				}
				foreach (var e in result.OneShots)
				{
					Console.WriteLine($"ONESHOT: {e}");
				}
			}
		}
	}
}
