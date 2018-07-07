using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CommandLineHelper
{
	public class CommandSet : List<Command>
	{
		public CommandSet(string helpCommandName = "help")
		{
			HelpCommandName = helpCommandName;
			Add(new Command
			{
				Name = helpCommandName,
				Run = a =>
				{
					foreach (var c in this)
					{
						if (!string.IsNullOrEmpty(c.HelpText))
						Config.TextWriter.WriteLine($"\t{c.Name}\t{c.HelpText}");
					}
				}
			});
		}

		public string OnUnknownCommand => $"Unknown command! Please type '{HelpCommandName}' for command details.";

		public string HelpCommandName { get; set; }

		public void Run(CommandContext context)
		{
			if (this.Count(c => c.Name == context.Command) > 1)
				throw new DuplicateNameException();

			var command = this.FirstOrDefault(c => c.Name == context.Command);

			if (command == null)
			{
				Config.TextWriter.WriteLine(OnUnknownCommand);
				return;
			}

			command.Run.Invoke(context);
		}
	}
}