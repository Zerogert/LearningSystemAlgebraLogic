using System;
namespace Learning_System_Algebra_logic.Data
{
	public class User
	{
		public User()
		{
			UserId = Guid.NewGuid().ToString();
		}
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string EMail { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public int NumberOfIterations { get; set; }
	}
}