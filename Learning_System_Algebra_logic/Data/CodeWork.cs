using System;

namespace Learning_System_Algebra_logic.Data
{
	public class CodeWork
	{
		public CodeWork()
		{
			CodeWorkId = Guid.NewGuid().ToString();
		}
		public string CodeWorkId { get; set; }
		public DateTime DateCreate { get; set; }
		public string Code { get; set; }
		public string State { get; set; }
		public string Result { get; set; }
		public DateTime DateComplete { get; set; }
		public Student Student { get; set; }
		public VariantWork VariantWork { get; set; }
	}
}