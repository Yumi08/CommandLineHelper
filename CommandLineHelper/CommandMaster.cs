using System.Collections.Generic;
using System.Linq;

namespace CommandLineHelper
{
	public class CommandMaster : List<CommandSet>
	{
		public CommandMaster(bool useDefaultHelpCommand = true, string defaultHelpCommandName = "sethelp")
		{
			UseDefaultHelpCommand = useDefaultHelpCommand;
			DefaultHelpCommandName = defaultHelpCommandName;
		}

		public bool UseDefaultHelpCommand { get; set; }

		public string DefaultHelpCommandName { get; set; }

		/// <summary>
		/// Runs a command. Should have been parsed by ParseMaster.
		/// </summary>
		/// <param name="icontext"></param>
		public void Run(InputContext icontext)
		{
			if (icontext == null)
				return;

			if (icontext.Set == DefaultHelpCommandName && UseDefaultHelpCommand)
			{
				PrintSetList();
				return;
			}

			var set = this.FirstOrDefault(s => s.Name == icontext.Set);

			if (set == null)
			{
				PrintSetList();
				return;
			}

			set.Run(icontext);
		}

		/// <summary>
		/// Parses direct input and runs it.
		/// </summary>
		/// <param name="input"></param>
		public void ParseAndRun(string input)
		{
			var icontext = Parser.ParseMaster(input);

			if (icontext == null)
				return;

			if (icontext.Set == DefaultHelpCommandName && UseDefaultHelpCommand)
			{
				PrintSetList();
				return;
			}

			var set = this.FirstOrDefault(s => s.Name == icontext.Set);

			if (set == null)
			{
				PrintSetList();
				return;
			}

			set.Run(icontext);
		}

		public void PrintSetList()
		{
			Config.TextWriter.WriteLine("SETS:");
			foreach (var set in this)
			{
				var helpText = set.Info == null ? $"\t{set.Name}" : $"\t{set.Name}    {set.Info}";

				Config.TextWriter.WriteLine(helpText);
			}
		}
	}
}