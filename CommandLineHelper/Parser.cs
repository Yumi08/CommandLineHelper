using System;
using System.Diagnostics.Contracts;

namespace CommandLineHelper
{
    public static class Parser
    {
		[Pure]
	    public static CommandContext Parse(string input)
	    {
			var context = new CommandContext();
		    var args = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

		    context.Command = args[0];

		    if (!args[1].StartsWith("-") && !args[1].StartsWith("--"))
		    {
			    for (var x = 1; x < args.Length; x++)
			    {
				    if (args[x].StartsWith("-") || args[x].StartsWith("--"))
					    break;
				    context.Value += args[x] + " ";
			    }
		    }

		    for (var i = 1; i < args.Length; i++)
		    {
			    if (args[i].StartsWith("--"))
			    {
				    if (i + 1 == args.Length ||
				        args[i + 1].StartsWith("--") ||
				        args[i + 1].StartsWith("-"))
					    context.OneShots.Add(args[i].Substring(2));

				    else context.Arguments.Add(args[i].Substring(2), args[i + 1]);
			    }

			    else if (args[i].StartsWith("-"))
			    {
				    if (i + 1 == args.Length ||
				        args[i + 1].StartsWith("--") ||
				        args[i + 1].StartsWith("-"))
					    context.OneShots.Add(args[i].Substring(1));

				    else context.Arguments.Add(args[i].Substring(1), args[i + 1]);
			    }
		    }

		    return context;
	    }
    }
}
