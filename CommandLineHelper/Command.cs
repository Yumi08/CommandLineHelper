using System;

namespace CommandLineHelper
{
	public class Command
	{
		public string Name { get; set; }

		public Action<CommandContext> Run { get; set; }

		public string HelpText { get; set; }
	}
}