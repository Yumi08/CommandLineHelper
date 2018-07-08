﻿using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommandLineHelper
{
	public class OptionSet : List<Option>
	{
		public new void Add(Option item)
		{
			if (this.Count(c => c.ShortForm == item.ShortForm || c.LongForm == item.LongForm) > 0)
				throw new DuplicateNameException();

			base.Add(item);
		}
	}
}