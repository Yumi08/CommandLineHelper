using System;
using System.IO;

namespace CommandLineHelper
{
	public static class Config
	{
		public static TextWriter TextWriter { get; set; } = Console.Out;
	}
}