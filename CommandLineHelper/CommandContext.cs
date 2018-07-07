using System.Collections.Generic;

namespace CommandLineHelper
{
	public class CommandContext
	{
		public string Command { get; set; }

		public string Value { get; set; }

		/// <summary>
		/// Options with parameters.
		/// </summary>
		public Dictionary<Option, string> Arguments { get; set; } = new Dictionary<Option, string>();

		/// <summary>
		/// Options without parameters.
		/// </summary>
		public List<Option> OneShots { get; set; } = new List<Option>();
	}
}