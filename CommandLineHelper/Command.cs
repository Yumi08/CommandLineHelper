using System;

namespace CommandLineHelper
{
	public class Command
	{
		public Command()
		{
		}

		public Command(string name)
		{
			Name = name;
		}

		public Command(string name, string helpText)
		{
			Name = name;
			HelpText = helpText;
		}

		public string Name { get; set; }

		public Action<CommandContext> Run { get; set; }

		public string HelpText { get; set; }

		public OptionSet OptionSet { get; set; }
	}
}