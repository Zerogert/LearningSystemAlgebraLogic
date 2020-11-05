using System;
using System.Collections.Generic;
namespace Learning_System_Algebra_logic.Data
{
	public class VariantWork
	{
		public VariantWork()
		{
			VariantWorkId = Guid.NewGuid().ToString();
		}
		public string VariantWorkId { get; set; }
		public string Variant { get; set; }
		public string Attachment { get; set; }
		public Work Work { get; set; }
		public ICollection<CodeWork> CodeWorks { get; set; }

		public override string ToString()
		{
			return $"{Work?.Name} {Variant}";
		}
	}
}