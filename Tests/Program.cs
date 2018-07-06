using System;
using CommandLineHelper;

namespace Tests
{
	class Program
	{
		static void Main()
		{
			var result = Parser.Parse("testcommand --arg1 param1 -arg2 param2");
			Console.WriteLine($"COMMAND: {result.Command}");
			foreach (var e in result.Arguments)
			{
				Console.WriteLine($"ARGUMENT: {e.Key}, VALUE: {e.Value}");
			}
		}
	}
}
