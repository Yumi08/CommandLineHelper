using System;
using System.Diagnostics.Contracts;

namespace CommandLineHelper
{
    public static class Parser
    {
		/// <summary>
		/// Parses an input intended to be ran directly by a command set.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		[Pure]
	    public static InputContext ParseSet(string input)
		{
			if (string.IsNullOrEmpty(input))
				return null;

			var context = new InputContext();
		    var args = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

		    context.Command = args[0];

		    if (args.Length > 1 && !args[1].StartsWith("-") && !args[1].StartsWith("--"))
		    {
			    for (var x = 1; x < args.Length; x++)
			    {
				    if (args[x].StartsWith("-") || args[x].StartsWith("--"))
					    break;
				    context.Value += args[x] + " ";
			    }

				// Removes the last space found after the value.
			    context.Value = context.Value.Substring(0, context.Value.Length - 1);
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

		/// <summary>
		/// Parses an input intended to be ran directly by a command master.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
	    public static InputContext ParseMaster(string input)
	    {
		    if (string.IsNullOrEmpty(input))
			    return null;

		    var context = new InputContext();
		    var args = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

		    context.Set = args[0];

		    if (args.Length <= 1)
			    return context;

		    context.Command = args[1];

		    if (args.Length > 2 && !args[2].StartsWith("-") && !args[2].StartsWith("--"))
		    {
			    for (var x = 2; x < args.Length; x++)
			    {
				    if (args[x].StartsWith("-") || args[x].StartsWith("--"))
					    break;
				    context.Value += args[x] + " ";
			    }

			    // Removes the last space found after the value.
			    context.Value = context.Value.Substring(0, context.Value.Length - 1);
		    }

		    for (var i = 2; i < args.Length; i++)
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
