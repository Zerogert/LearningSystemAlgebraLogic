using System.Windows.Input;
using Learning_System_Algebra_logic.Pages;

namespace Learning_System_Algebra_logic.ViewModels
{
	internal class ResultWorkViewModel : BaseViewModel
	{
		private int countSchemes;
		private int countTrue;
		private ICommand exitCommand;
		private int mark;
		private string nameStudent;
		private int procent;
		private ICommand showErrorsCommand;

		public ResultWorkViewModel(string student, int countSchemes, int countTrue)
		{
			NameStudent = student;
			CountSchemes = countSchemes;
			CountTrue = countTrue;
			Procent = (int) ((float) countTrue / countSchemes * 100);
			Mark = MakeMark(Procent);
		}

		public ICommand ShowErrorsCommand
		{
			get
			{
				showErrorsCommand = showErrorsCommand ?? new Command(ShowErrors);
				return showErrorsCommand;
			}
		}

		public ICommand ExitCommand
		{
			get
			{
				exitCommand = exitCommand ?? new Command(Exit);
				return exitCommand;
			}
		}

		public string NameStudent
		{
			get => nameStudent;
			set
			{
				nameStudent = value;
				OnPropertyChanged();
			}
		}

		public int CountSchemes
		{
			get => countSchemes;
			set
			{
				countSchemes = value;
				OnPropertyChanged();
			}
		}

		public int CountTrue
		{
			get => countTrue;
			set
			{
				countTrue = value;
				OnPropertyChanged();
			}
		}

		public int Mark
		{
			get => mark;
			set
			{
				mark = value;
				OnPropertyChanged();
			}
		}

		public int Procent
		{
			get => procent;
			set
			{
				procent = value;
				OnPropertyChanged();
			}
		}

		private int MakeMark(int procent)
		{
			if (procent > 55)
			{
				if (procent > 70)
				{
					if (procent > 85)
						return 5;
					return 4;
				}

				return 3;
			}

			return 2;
		}

		private void ShowErrors()
		{
		}

		private void Exit()
		{
			ManagerPage.ChangePage("Main", new Navigation {DataContext = new NavigationViewModel("Student")});
		}
	}
}