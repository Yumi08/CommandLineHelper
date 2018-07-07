namespace CommandLineHelper
{
	public class Option
	{
		public Option()
		{
			
		}

		public Option(string name)
		{
			Name = name;
		}

		public Option(string name, string helpText)
		{
			Name = name;
			HelpText = helpText;
		}

		public Option(string name, string paramType, string helpText)
		{
			Name = name;
			ParamType = paramType;
			HelpText = helpText;
		}

		/// <summary>
		/// For a short form, separate the string with a "|". The short form will come first.
		/// </summary>
		public string Name { get; set; }

		public string ParamType { get; set; }

		public string ShortForm => Name.Split('|')[0];

		public string LongForm => Name.Split('|')[1];

		public bool Optional { get; set; } = true;

		public string Default { get; set; }

		public string HelpText { get; set; }
	}
}