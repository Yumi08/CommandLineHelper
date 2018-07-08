using System;
using System.Diagnostics;
using CommandLineHelper;

namespace Tests
{
	class Program
	{
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
							}
						}
					}
				},
				new CommandSet("debug")
				{
					new Command("run", "Runs the debug.")
					{
						Run = c =>
						{
							if (c.OneShotEnabled("tee"))
								Console.WriteLine("We are debugging!");

							Console.WriteLine("We are debugging!");
						},
						OptionSet = new OptionSet
						{
							new Option("tee"),
							new Option("t|tee")
						}
					}
				}
			};

			while (true)
			{
				master.ParseAndRun(Console.ReadLine());
			}
		}
	}
}
