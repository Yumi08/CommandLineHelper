using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CommandLineHelper
{
	public class CommandSet : List<Command>
	{
		public CommandSet(bool useDefaultHelpCommand = true, string defaultHelpCommandName = "help")
		{
			OnUnknownCommand = "Unknown command!";

			if (useDefaultHelpCommand)
				OnUnknownCommand += $" Please type '{defaultHelpCommandName}' for command details.";

			DefaultHelpCommandName = defaultHelpCommandName;

			if (useDefaultHelpCommand)
				Add(new Command
				{
					Name = defaultHelpCommandName,
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

		public string OnUnknownCommand { get; set; }

		public string DefaultHelpCommandName { get; set; }

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