using Learning_System_Algebra_logic.Data;

namespace Learning_System_Algebra_logic.ViewModels.DataViewModel
{
	public class StudentViewModel
	{
		public StudentViewModel(Student student)
		{
			Student = student;
			Id = student.StudentId;
			FirstName = student.FirstName;
			LastName = student.LastName;
			MiddleName = student.MiddleName;
			Group = student?.Group?.ToString();
			Note = student.Note;
		}

		public StudentViewModel()
		{
		}

		public Student Student { get; set; }
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Group { get; set; }
		public string Note { get; set; }
	}
}