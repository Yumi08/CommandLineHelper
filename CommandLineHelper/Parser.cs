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

		    for (var i = 1; i < args.Length; i++)
		    {
			    if (args[i].StartsWith("--"))
			    {
				    context.Arguments.Add(args[i].Substring(2), args[i+1]);
			    }

				else if (args[i].StartsWith("-"))
			    {
				    context.Arguments.Add(args[i].Substring(1), args[i+1]);
			    }
		    }

		    return context;
	    }
    }
}
