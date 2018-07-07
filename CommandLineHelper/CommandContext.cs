using System.Collections.Generic;
using System.Linq;

namespace CommandLineHelper
{
	public class CommandContext
	{
		public Command Command { get; set; }

		public string CommandName { get; set; }

		public string Value { get; set; }

		/// <summary>
		/// Options with parameters.
		/// </summary>
		public Dictionary<Option, string> Arguments { get; set; } = new Dictionary<Option, string>();

		/// <summary>
		/// Options without parameters.
		/// </summary>
		public List<Option> OneShots { get; set; } = new List<Option>();

		/// <summary>
		/// Get the parameter given for a specific option, or its default if it's not given.
		/// </summary>
		/// <param name="optionName"></param>
		/// <returns></returns>
		public string GetOptionParam(string optionName)
		{
			var optionType = Command.OptionSet.FirstOrDefault(o => o.Name == optionName);
			var option = Arguments.FirstOrDefault(o => o.Key == optionType);

			if (option.Key == null)
				return Command.OptionSet.First(o => o.Name == optionName).Default;

			return option.Value;
		}

		public bool OneShotEnabled(string optionName)
		{
			if (OneShots.Any(o => o.Name == optionName)) return true;

			return false;
		}
	}
}