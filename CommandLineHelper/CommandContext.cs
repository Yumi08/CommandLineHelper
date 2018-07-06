using System.Collections.Generic;

namespace CommandLineHelper
{
	public class CommandContext
	{
		public string Command { get; set; }

		public Dictionary<string, string> Arguments { get; set; } = new Dictionary<string, string>();
	}
}