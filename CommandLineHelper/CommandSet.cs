using System;
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
						if (string.IsNullOrEmpty(a.Value))
							foreach (var c in this)
							{
								if (!string.IsNullOrEmpty(c.HelpText))
									Config.TextWriter.WriteLine($"\t{c.Name}\t{c.HelpText}");
							}

						else
						{
							// If the command doesn't exist, tell the user.
							if (this.All(c => c.Name != a.Value))
							{
								Config.TextWriter.WriteLine(OnUnknownCommand);
								return;
							}

							// Don't let the user call the help command
							if (a.Value == defaultHelpCommandName)
							{
								Config.TextWriter.WriteLine($"'{defaultHelpCommandName}' allows you to see info on commands.");
								return;
							}

							var command = this.First(c => c.Name == a.Value);

							Config.TextWriter.WriteLine($"\t{command.HelpText}");

							foreach (var o in command.OptionSet)
							{
								var helpText = o.HelpText ?? "";
								helpText = helpText != null ? $"\t{helpText}\t" : "\t";
								Config.TextWriter.WriteLine($"\t\t{o.Name}{helpText}Required: {!o.Optional}");
							}
						}
					}
				});
		}

		public new void Add(Command item)
		{
			// Prevents duplicate commands.
			if (this.Count(c => c.Name == item.Name) > 0)
				throw new DuplicateNameException();

			base.Add(item);
		}

		public string OnUnknownCommand { get; set; }

		public string DefaultHelpCommandName { get; set; }

		public void Run(CommandContext context)
		{
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