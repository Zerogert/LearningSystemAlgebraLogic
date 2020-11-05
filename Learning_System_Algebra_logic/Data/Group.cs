using System;
using System.Collections.Generic;

namespace Learning_System_Algebra_logic.Data
{
	public class Group
	{
		public Group()
		{
			GroupId = Guid.NewGuid().ToString();
		}
		public string GroupId { get; set; }
		public string Name { get; set; }
		public int DateCreate { get; set; }
		public ICollection<Student> Students { get; set; }
		public override string ToString()
		{
			if (Name == "") return Name;
			return Name + " (" + DateCreate + " год)";
		}
	}
}