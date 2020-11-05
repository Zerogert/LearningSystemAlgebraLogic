using System;
using System.Collections.Generic;
namespace Learning_System_Algebra_logic.Data
{
	public class Student
	{
		public Student()
		{
			StudentId = Guid.NewGuid().ToString();
		}
		public string StudentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Note { get; set; }

		public Group Group { get; set; }
		public ICollection<CodeWork> CodeWorks { get; set; }

		public override string ToString()
		{
			return $"{FirstName} {LastName} {MiddleName}";
		}
	}
}