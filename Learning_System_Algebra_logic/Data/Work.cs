using System;
using System.Collections.Generic;

namespace Learning_System_Algebra_logic.Data
{
	public class Work
	{
		public Work()
		{
			WorkId = Guid.NewGuid().ToString();
		}

		public string WorkId { get; set; }
		public string Name { get; set; }
		public string TypeWork { get; set; }
		public ICollection<VariantWork> VariantWorks { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}