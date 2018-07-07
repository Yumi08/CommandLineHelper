using System.Collections.Generic;
using System.Data;
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

							// Special exception for calling 'help help'
							if (a.Value == defaultHelpCommandName)
							{
								Config.TextWriter.WriteLine($"'{defaultHelpCommandName}' allows you to see info on commands.");
								return;
							}

							PrintHelpOnCommand(this.First(c => c.Name == a.Value));
						}
					}
				});
		}

		private void PrintHelpOnCommand(Command command)
		{
			Config.TextWriter.WriteLine($"\t{command.HelpText}");

			if (command.OptionSet == null)
				return;
			foreach (var o in command.OptionSet)
			{
				var helpText = o.HelpText;
				helpText = helpText != null ? $"\t{helpText}\t" : "\t";
				var paramType = " " + o.ParamType ?? "";
				Config.TextWriter.WriteLine($"\t\t{o.Name}{paramType}{helpText}Required: {!o.Optional}");
			}
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

		public void Run(InputContext icontext)
		{
			if (icontext == null)
				return;

			var command = this.FirstOrDefault(c => c.Name == icontext.Command);

			if (command == null)
			{
				Config.TextWriter.WriteLine(OnUnknownCommand);
				return;
			}

			var ccontext = ConvertIContextToCContext(icontext, command);

			if (ccontext != null)
				command.Run.Invoke(ccontext);
		}

		private CommandContext ConvertIContextToCContext(InputContext icontext, Command command)
		{
			var ccontext = new CommandContext
			{
				Command = command,
				Value = icontext.Value
			};

			foreach (var a in icontext.Arguments)
			{
				// If a non-existent option is attempted to be used, show command help.
				if (command.OptionSet.All(o => o.ShortForm != a.Key && o.LongForm != a.Key))
				{
					PrintHelpOnCommand(command);
					return null;
				}

				ccontext.Arguments.Add(command.OptionSet.First(o => o.ShortForm == a.Key || o.LongForm == a.Key), a.Value);
			}

			if (command.OptionSet != null &&
				command.OptionSet.Count(o => o.Optional == false) !=
			    ccontext.Arguments.Count(a => a.Key.Optional == false))
			{
				PrintHelpOnCommand(command);
				return null;
			}

			foreach (var os in icontext.OneShots)
			{
				if (command.OptionSet == null) break;

				if (command.OptionSet.All(o => o.ShortForm != os && o.LongForm != os))
				{
					PrintHelpOnCommand(command);
					return null;
				}

				ccontext.OneShots.Add(command.OptionSet.First(o => o.ShortForm == os || o.LongForm == os));
			}

			return ccontext;
		}
	}
}