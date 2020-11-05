using System.Windows.Input;
using Learning_System_Algebra_logic.Pages;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class SelectUserViewModel : BaseViewModel
	{
		private ICommand selectedStudentCommand;


		private ICommand selectedTeacherCommand;

		public ICommand SelectedStudentCommand
		{
			get
			{
				selectedStudentCommand = selectedStudentCommand ?? new Command(SelectedStudent);
				return selectedStudentCommand;
			}
		}

		public ICommand SelectedTeacherCommand
		{
			get
			{
				selectedTeacherCommand = selectedTeacherCommand ?? new Command(SelectedTeacher);
				return selectedTeacherCommand;
			}
		}

		public void SelectedStudent()
		{
			ManagerPage.ChangePage("Main", new Navigation {DataContext = new NavigationViewModel("Student")});
		}

		public void SelectedTeacher()
		{
			ManagerPage.ChangePage("Main", new AuthorizationPage {DataContext = new AutorizationViewModel()});
		}
	}
}