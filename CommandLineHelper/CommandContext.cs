using System.Collections.Generic;

namespace CommandLineHelper
{
	public class CommandContext
	{
		public string Command { get; set; }

		/// <summary>
		/// Arguments/options with parameters.
		/// </summary>
		public Dictionary<string, string> Arguments { get; set; } = new Dictionary<string, string>();

		/// <summary>
		/// Arguments/Options without parameters.
		/// </summary>
		public List<string> OneShots { get; set; } = new List<string>();
	}
}