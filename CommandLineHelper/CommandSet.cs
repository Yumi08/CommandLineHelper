using System.Collections.Generic;
using System.Linq;

namespace CommandLineHelper
{
	public class CommandSet : List<Command>
	{
		public string OnError { get; set; }

		public void Run(CommandContext context)
		{
			var command = this.First(c => c.Name == context.Command);
			command.Run.Invoke(context);
		}
	}
}